using System.Text;
using System.Text.Json.Nodes;
using AlgoTecture.AICoreService.Application.Services;
using AlgoTecture.GeoAdminSearch;
using AlgoTecture.Reservation.Contracts;
using AlgoTecture.Reservation.Contracts.Dto;
using AlgoTecture.Reservation.Contracts.Requests;
using AlgoTecture.Space.Contracts;
using AlgoTecture.TelegramBot.Api.Controllers.Base;
using AlgoTecture.TelegramBot.Application;
using AlgoTecture.TelegramBot.Application.Helpers;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Domain;
using AlgoTecture.User.Contracts;
using AlgoTecture.User.Contracts.Requests;
using AlgoTecture.Utils;
using Deployf.Botf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class MainController : ReservationControllerBase
{
    private readonly IUserAuthenticationService _authService;
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    private readonly GeoAdminSettings _geoAdminSettings;
    private readonly IUserCarsApi _userCarsApi;
    private readonly IUserCache _cache;
    private readonly ISpaceApi _spaceApi;
    private readonly IReservationApi _reservationApi;
    private readonly DeepSeekSettings _deepSeekSettings;
    private readonly ILogger<MainController> _logger;

    public MainController(IUserAuthenticationService authService, IReservationFlowService flow,
        IGeoAdminSearcher geoAdminSearcher, IOptions<GeoAdminSettings> geoAdminSettings, IUserCarsApi userCarsApi,
        IUserCache cache, ISpaceApi spaceApi, IReservationApi reservationApi,
        IOptions<DeepSeekSettings> deepSeekSettings, ILogger<MainController> logger)
        : base(flow)
    {
        _authService = authService;
        _geoAdminSearcher = geoAdminSearcher;
        _userCarsApi = userCarsApi;
        _cache = cache;
        _spaceApi = spaceApi;
        _reservationApi = reservationApi;
        _geoAdminSettings = geoAdminSettings.Value;
        _deepSeekSettings = deepSeekSettings.Value;
        _logger = logger;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        try
        {
            var chatId = Context.GetSafeChatId();
            var userId = Context.GetSafeUserId();
            var userName = Context.GetUsername();
            var userFullName = Context.GetUserFullName();
            if (userId == null) return;

            _logger.LogInformation("Telegram user {UserId} started the bot. Username: {Username}, FullName: {FullName}",
                userId, userName, userFullName);

            var sessionState = new BotSessionState
            {
                CurrentReservation = new ReservationDraft { SelectedSpaceTypeId = 1 }
            };

            await DeletePreviousMessageIfNeeded(sessionState, chatId!.Value);

            var linkedUserId = await _authService.EnsureUserAuthenticatedAsync(
                userId.Value, chatId.Value, userFullName, userName);

            if (linkedUserId == Guid.Empty)
            {
                _logger.LogWarning("User {UserId} failed to authenticate. May be first. Task.Delay(1000)", userId);
                await Task.Delay(1000);
            }

            _logger.LogInformation("Telegram user {UserId} linked to system user {LinkedUserId}", userId, linkedUserId);

            PushL("I am your parking 🅿️ assistant. I help you find and manage spots near you.");
            RowButton("🚗 reserve a parking", Q(SelectRentalTime, sessionState, TimeSelectionStage.None, null!));
            RowButton("📅 manage reservations", Q(ShowReservations, sessionState));

            var msg = await SendOrUpdate();
            sessionState.MessageId = msg!.MessageId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in /start");
            throw;
        }
    }

    [Action]
    public async Task SelectRentalTime(BotSessionState sessionState, TimeSelectionStage stage, DateTime? dateTime)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var userId = Context.GetSafeUserId();

        var time = string.Empty;
        if (dateTime != null)
        {
            PushL("Enter the rental start time (in HH:mm format, e.g., 14:15)");
            var message = await SendOrUpdate();
            sessionState.MessageId = message!.MessageId;

            time = await AwaitText(() => Send("Text input timeout. Use /start to try again"));
            await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
        }

        sessionState.CurrentReservation.PendingStartRentLocal = stage == TimeSelectionStage.Start
            ? DateTimeParser.GetValidDateTime(dateTime, time)
            : sessionState.CurrentReservation.PendingStartRentLocal;
        sessionState.CurrentReservation.PendingEndRentLocal = stage == TimeSelectionStage.End
            ? DateTimeParser.GetValidDateTime(dateTime, time)
            : sessionState.CurrentReservation.PendingEndRentLocal;

        var start = sessionState.CurrentReservation.PendingStartRentLocal;
        var end = sessionState.CurrentReservation.PendingEndRentLocal;

        if (start is not null && end is not null && end <= start)
        {
            _logger.LogWarning("User {UserId} selected invalid period: start {Start} >= end {End}", userId, start, end);
            sessionState.CurrentReservation.PendingEndRentLocal = null;
        }

        RowButton(start is not null
            ? $"{start:dddd, MMMM dd yyyy HH:mm}"
            : "⏱️ start time", Q(PressToChooseTheDate, sessionState, TimeSelectionStage.Start));
        RowButton(end is not null
            ? $"{end:dddd, MMMM dd yyyy HH:mm}"
            : "end time⏱️", Q(PressToChooseTheDate, sessionState, TimeSelectionStage.End));

        if (start != null && end != null)
            RowButton("📍 where to park?", Q(EnterAddress, sessionState));

        RowButton("↩️ go back", Q(Start));

        if (string.IsNullOrEmpty(time))
        {
            PushL("When do you want to park? Pick start and end time.");
            await SendOrUpdate();
        }
        else
        {
            await Send("When do you want to park? Pick start and end time.");
        }
    }

    [Action]
    private async Task PressToChooseTheDate(BotSessionState sessionState, TimeSelectionStage stage)
    {
        await Calendar("", sessionState, false, stage);
    }

    [Action]
    private async Task Calendar(string state, BotSessionState sessionState, bool isNavigateBetweenMonths,
        TimeSelectionStage stage)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var now = DateTime.UtcNow;

        var calendar = new CalendarMessageBuilder();
        if (isNavigateBetweenMonths)
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, sessionState, true, stage))
                .OnSelectPath(date =>
                    Q(SelectRentalTime, sessionState, stage, date));
        }
        else
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, sessionState, true, stage))
                .OnSelectPath(date =>
                    Q(SelectRentalTime, sessionState, stage, date))
                .SkipDay(d => d.Day < now.Day);
        }

        calendar.Build(Message, new PagingService());

        RowButton("↩️ go back", Q(SelectRentalTime, sessionState, TimeSelectionStage.None, null!));
        PushL("choose the day you’ll start parking");
        var message = await SendOrUpdate();
        sessionState.MessageId = message!.MessageId;
    }

    [Action]
    public async Task EnterAddress(BotSessionState sessionState)
    {
        try
        {
            var chatId = Context.GetSafeChatId();
            if (!chatId.HasValue) return;

            var userId = Context.GetSafeUserId();
            PushL("Enter the address or part of the address (or type 'back' to return)");
            var message = await SendOrUpdate();
            sessionState.MessageId = message!.MessageId;

            var address = await AwaitText(() => Send("Text input timeout. Use /start to try again"));
            _logger.LogInformation("User {UserId} entered address query: {Address}", userId, address);

            if (address.Equals("back", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("User {UserId} returned from address entry", userId);
                await Call<MainController>(m => m.SelectRentalTime(sessionState, TimeSelectionStage.None, null!));
                return;
            }

            await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);

            var baseUrl = _geoAdminSettings.GeoAdminBaseUrl;
            var labels = (await _geoAdminSearcher.GetAddress(baseUrl, address)).ToList();

            if (!labels.Any())
            {
                _logger.LogWarning("No address matches found for user {UserId} and query '{Address}'", userId, address);
                RowButton("Try again", Q(EnterAddress, sessionState));
                await SendOrUpdate();
                return;
            }

            foreach (var label in labels)
            {
                RowButton(label.label, Q(ShowNearestSpaces, sessionState, new GeoAddressInput
                {
                    FeatureId = label.featureId,
                    NormalizedAddress = label.label,
                    OriginalInput = new Point(label.lat, label.lon)
                }));
            }

            RowButton("↩️ go back", Q(SelectRentalTime, sessionState, TimeSelectionStage.None, null!));

            var msg = await Send("Choose the right address");
            sessionState.MessageId = msg.MessageId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in /EnterAddress");
            throw;
        }
    }

    [Action]
    public async Task ShowNearestSpaces(BotSessionState sessionState, GeoAddressInput geoAddressInput)
    {
        try
        {
            var userId = Context.GetSafeUserId();
            _logger.LogInformation("User {UserId} searching nearest spaces for {Address}",
                userId, geoAddressInput.NormalizedAddress);

            var spaces = await _spaceApi.GetNearestSpacesAsync(
                geoAddressInput.OriginalInput.X,
                geoAddressInput.OriginalInput.Y,
                spaceTypeId: 1,
                maxDistanceMeters: 100000, count: 7);

            if (spaces.Count == 0)
            {
                _logger.LogWarning("No nearby spaces found for user {UserId}", userId);
                await Send("No nearby spaces found 😔");
                return;
            }

            foreach (var space in spaces)
            {
                string? parkingType = string.Empty;
                if (space.SpaceProperties.IsEmptyJson()) continue;
                if (space.SpaceProperties != null)
                {
                    var node = JsonNode.Parse(space.SpaceProperties);
                    parkingType = node?["Type"]?.GetValue<string>();
                }

                if (space is { Latitude: null, Longitude: null }) continue;

                var geoAddressInputToNearestPoint = new GeoAddressInput
                {
                    FeatureId = geoAddressInput.FeatureId,
                    NormalizedAddress = geoAddressInput.NormalizedAddress,
                    OriginalInput = geoAddressInput.OriginalInput,
                    Location = new Point(space.Latitude!.Value, space.Longitude!.Value),
                    Type = parkingType
                };

                var newSessionState = new BotSessionState
                {
                    MessageId = sessionState.MessageId,
                    LocationMessageId = sessionState.LocationMessageId,
                    CurrentReservation = new ReservationDraft
                    {
                        SelectedSpaceId = space.Id,
                        SelectedSpaceTypeId = space.SpaceTypeId,
                        PendingStartRentLocal = sessionState.CurrentReservation.PendingStartRentLocal,
                        PendingEndRentLocal = sessionState.CurrentReservation.PendingEndRentLocal,
                        SpaceTimeZone = space.TimeZoneId,
                        SelectedCarNumber = sessionState.CurrentReservation.SelectedCarNumber,
                        GeoAddressInput = geoAddressInputToNearestPoint
                    }
                };

                int? roundedDistanceInMeters =
                    space.DistanceMeters.HasValue ? (int)Math.Round(space.DistanceMeters.Value) : null;

                RowButton($"{parkingType} parking — {roundedDistanceInMeters} m",
                    Q(ShowDetails, newSessionState));
            }

            RowButton("↩️ go back", Q(EnterAddress, sessionState));

            PushL("🅿️ Available parking");

            await DeletePreviousMessageIfNeeded(sessionState, Context.GetSafeChatId()!.Value);
            await DeletePreviousLocationMessageIfNeeded(sessionState, Context.GetSafeChatId()!.Value);

            var textMessage = await Send();
            sessionState.MessageId = textMessage!.MessageId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in /ShowNearestSpaces");
            throw;
        }
    }

    [Action]
    public async Task ShowDetails(BotSessionState sessionState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
        var locationMessage = await Client.SendLocationAsync(
            chatId: chatId.Value,
            latitude: (float)sessionState.CurrentReservation.GeoAddressInput.Location.Y,
            longitude: (float)sessionState.CurrentReservation.GeoAddressInput.Location.X,
            replyMarkup: new InlineKeyboardMarkup([
                [
                    InlineKeyboardButton.WithCallbackData("🅿️ make a reservation",
                        Q(MakeAReservation, sessionState))
                ],

                [
                    InlineKeyboardButton.WithCallbackData("↩️ go back",
                        Q(ShowNearestSpaces, sessionState, sessionState.CurrentReservation.GeoAddressInput))
                ]
            ])
        );

        sessionState.LocationMessageId = locationMessage.MessageId;

        PushL("Here is the parking spot 📍");
        var textMessage = await SendOrUpdate();
        sessionState.MessageId = textMessage!.MessageId;
    }

    [Action]
    public async Task MakeAReservation(BotSessionState sessionState)
    {
        try
        {
            var chatId = Context.GetSafeChatId();
            var userId = Context.GetSafeUserId();
            if (userId == null) return;

            var cachedLinkedUserId = await _cache.GetUserIdByTelegramAsync(userId.Value);

            if (cachedLinkedUserId == Guid.Empty)
            {
                PushL("User not found in cache. Please /start again.");
                await SendOrUpdate();
                return;
            }

            var userCarsDto = await _userCarsApi.GetCarNumbersAsync(cachedLinkedUserId!.Value);

            await DeletePreviousMessageIfNeeded(sessionState, chatId!.Value);
            await DeletePreviousLocationMessageIfNeeded(sessionState, chatId.Value);

            if (userCarsDto.CarNumbers.Count == 0)
            {
                PushL("You have no car numbers yet 🚗");

                var message = await Send();
                sessionState.MessageId = message!.MessageId;
                await Client.SendTextMessageAsync(chatId.Value,
                    "Add your first car number:",
                    replyMarkup: new InlineKeyboardMarkup([
                        [
                            InlineKeyboardButton.WithCallbackData("➕ add car number", Q(AddCarNumber, sessionState)),
                            InlineKeyboardButton.WithCallbackData("↩️ go back", Q(ShowDetails, sessionState))
                        ]
                    ]));
                return;
            }

            var buttons = userCarsDto.CarNumbers
                .Select(curNumber => new[]
                {
                    InlineKeyboardButton.WithCallbackData($"🚘 {curNumber}",
                        Q(ShowReservationSummary, sessionState, curNumber))
                })
                .ToList();

            buttons.Add([InlineKeyboardButton.WithCallbackData("➕ add new", Q(AddCarNumber, sessionState))]);

            buttons.Add([InlineKeyboardButton.WithCallbackData("↩️ go back", Q(ShowDetails, sessionState))]);

            var msg = await Client.SendTextMessageAsync(chatId.Value,
                "Select to confirm a car number for reservation:",
                replyMarkup: new InlineKeyboardMarkup(buttons));
            sessionState.MessageId = msg.MessageId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in /MakeAReservation");
            throw;
        }
    }

    [Action]
    public async Task AddCarNumber(BotSessionState sessionState)
    {
        try
        {
            var chatId = Context.GetSafeChatId();
            var userId = Context.GetSafeUserId();
            if (userId == null) return;
            if (!chatId.HasValue) return;

            PushL("Please enter your car number (e.g., ZH12345):");
            await SendOrUpdate();

            var input = await AwaitText(() => Send("Timeout. Use /start to try again"));
            if (string.IsNullOrWhiteSpace(input)) return;

            var cachedLinkedUserId = await _cache.GetUserIdByTelegramAsync(userId.Value);

            var userCarsDto =
                await _userCarsApi.AddCarNumberAsync(cachedLinkedUserId!.Value, new AddCarNumberRequest(input));

            PushL($"✅ Added: {input}. You now have {userCarsDto.CarNumbers.Count} car(s).");
            await SendOrUpdate();

            await Call<MainController>(m => m.MakeAReservation(sessionState));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in /AddCarNumber");
            throw;
        }
    }

    [Action]
    public async Task ShowReservationSummary(BotSessionState sessionState, string carNumber)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var draft = sessionState.CurrentReservation;
        if (draft.PendingStartRentLocal == null || draft.PendingEndRentLocal == null)
        {
            await Send("⚠️ Missing reservation data. Please start over with /start");
            return;
        }

        await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
        await DeletePreviousLocationMessageIfNeeded(sessionState, chatId.Value);

        var locationMessage = await Client.SendLocationAsync(
            chatId: chatId.Value,
            latitude: (float)sessionState.CurrentReservation.GeoAddressInput.Location.Y,
            longitude: (float)sessionState.CurrentReservation.GeoAddressInput.Location.X
        );
        sessionState.LocationMessageId = locationMessage.MessageId;

        var summaryText = $"""
                           🧾 <b>Reservation summary</b>

                           📍 <b>Address:</b> {sessionState.CurrentReservation.GeoAddressInput.NormalizedAddress}
                           🚗 <b>Car:</b> {carNumber}
                           🕒 <b>From:</b> {draft.PendingStartRentLocal:dd.MM.yyyy HH:mm}
                           🕒 <b>To:</b> {draft.PendingEndRentLocal:dd.MM.yyyy HH:mm}
                           🏷 <b>Space type:</b> {sessionState.CurrentReservation.GeoAddressInput.Type}
                           💰 <b>Price:</b> will be calculated after confirmation
                           """;

        var markup = new InlineKeyboardMarkup([
            [
                InlineKeyboardButton.WithCallbackData("✅ confirm", Q(ConfirmReservation, sessionState, carNumber))
            ],
            [
                InlineKeyboardButton.WithCallbackData("↩️ go back", Q(MakeAReservation, sessionState))
            ]
        ]);

        var textMessage = await Client.SendTextMessageAsync(
            chatId: chatId.Value,
            text: summaryText,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
            replyMarkup: markup
        );
        sessionState.MessageId = textMessage.MessageId;
    }

    [Action]
    public async Task ConfirmReservation(BotSessionState sessionState, string carNumber)
    {
        try
        {
            var chatId = Context.GetSafeChatId();
            var userId = Context.GetSafeUserId();
            if (userId == null || !chatId.HasValue) return;

            _logger.LogInformation("User {UserId} confirming reservation for space {SpaceId} with car {CarNumber}",
                userId, sessionState.CurrentReservation.SelectedSpaceId, carNumber);

            var linkedUserId = await _cache.GetUserIdByTelegramAsync(userId.Value);
            if (linkedUserId == Guid.Empty)
            {
                _logger.LogWarning("Linked user not found for Telegram user {UserId}", userId);
                await Send("⚠️ Session expired. Please /start again.");
                return;
            }

            var draft = sessionState.CurrentReservation;
            draft.SelectedCarNumber = carNumber;
            if (draft.PendingStartRentLocal == null || draft.PendingEndRentLocal == null)
            {
                await Send("⚠️ Missing reservation data.");
                return;
            }

            var tz = TimeZoneInfo.FindSystemTimeZoneById(draft.SpaceTimeZone!);

            var startUtc = TimeZoneInfo.ConvertTimeToUtc(draft.PendingStartRentLocal.Value, tz);
            var endUtc = TimeZoneInfo.ConvertTimeToUtc(draft.PendingEndRentLocal.Value, tz);

            var request = new CreateReservationRequest(draft.SelectedSpaceId, linkedUserId!.Value, startUtc, endUtc,
                draft.SelectedCarNumber);

            ReservationDto result = null;
            try
            {
                result = await _reservationApi.CreateReservation(request);
                if (result.Id == Guid.Empty)
                {
                    _logger.LogWarning("Reservation conflict: space {SpaceId} already reserved", draft.SelectedSpaceId);
                    await Send("Sorry, but it seems this time is busy.");
                }
                else
                {
                    _logger.LogInformation("Reservation created successfully: {ReservationId} for user {UserId}",
                        result.Id, userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reservation for user {UserId}", userId);
                await Send("⚠️ Reservation failed. Please try again later.");
            }

            await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
            await DeletePreviousLocationMessageIfNeeded(sessionState, chatId.Value);
            if (result!.Id == Guid.Empty)
            {
                var markupWithNullResult = new InlineKeyboardMarkup([
                    [
                        InlineKeyboardButton.WithCallbackData("↩️ go to reservation summary",
                            Q(ShowReservationSummary, sessionState, carNumber))
                    ]
                ]);

                var message = await Client.SendTextMessageAsync(
                    chatId: chatId.Value,
                    text: "Sorry, but it seems this time is busy.",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                    replyMarkup: markupWithNullResult
                );

                sessionState.MessageId = message.MessageId;
            }
            else
            {
                var confirmation = $"""
                                    ✅ <b>Reservation confirmed!</b>
                                    📍 {sessionState.CurrentReservation.GeoAddressInput.NormalizedAddress}
                                    🚗 {carNumber}
                                    🕒 {draft.PendingStartRentLocal:dd.MM.yyyy HH:mm} – {draft.PendingEndRentLocal:dd.MM.yyyy HH:mm}
                                    🆔 Reservation ID {result.PublicId}: 
                                    """;

                var markup = new InlineKeyboardMarkup([
                    [
                        InlineKeyboardButton.WithCallbackData("↩️ go to reservations",
                            Q(ShowReservations, sessionState))
                    ]
                ]);

                var message = await Client.SendTextMessageAsync(
                    chatId: chatId.Value,
                    text: confirmation,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                    replyMarkup: markup
                );

                sessionState.MessageId = message.MessageId;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in /ConfirmReservation");
            throw;
        }
    }

    [Action]
    public async Task ShowReservations(BotSessionState sessionState)
    {
        try
        {
            var chatId = Context.GetSafeChatId();
            var userId = Context.GetSafeUserId();
            if (userId == null || !chatId.HasValue) return;

            await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);

            var linkedUserId = await _cache.GetUserIdByTelegramAsync(userId.Value);
            if (linkedUserId == Guid.Empty)
            {
                await Send("⚠️ Session expired. Please /start again.");
                return;
            }

            var activeReservations = await _reservationApi.GetUserReservations(linkedUserId);

            if (activeReservations.Count == 0)
            {
                var msg = await Client.SendTextMessageAsync(chatId.Value,
                    "😔 You have no active reservations.",
                    replyMarkup: new InlineKeyboardMarkup([
                        [InlineKeyboardButton.WithCallbackData("↩️ go to start", Q(Start))]
                    ])
                );
                sessionState.MessageId = msg.MessageId;
                return;
            }

            var summary = new StringBuilder();
            summary.AppendLine("<b>📋 Your active reservations</b>\n");

            foreach (var r in activeReservations)
            {
                summary.AppendLine($"""
                                    🆔 <b>{r.PublicId}</b>
                                    🚗 {r.CarNumber}
                                    🏷 <i>{r.Status}</i>
                                    """);
                summary.AppendLine();
            }

            var markup = new InlineKeyboardMarkup([
                [InlineKeyboardButton.WithCallbackData("↩️ back to start menu", Q(Start))]
            ]);

            var message = await Client.SendTextMessageAsync(
                chatId.Value,
                summary.ToString(),
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                replyMarkup: markup
            );

            sessionState.MessageId = message.MessageId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in /ShowReservations");
            throw;
        }
    }

    [On(Handle.Exception)]
    public void Exception(Exception ex)
    {
        _logger.LogError(ex, "Handle.Exception on telegram-bot");
    }

    [On(Handle.ChainTimeout)]
    void ChainTimeout(Exception ex)
    {
        _logger.LogError(ex, "Handle.Exception on telegram-bot");
    }

    [On(Handle.Unknown)]
    public async Task Unknown()
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        if (Context.Update.Message?.Text is { } messageText)
        {
            _logger.LogInformation("AI flow triggered by user {UserId} message: {Message}",
                Context.GetSafeUserId(), messageText);

            var progressMsg = await Client.SendTextMessageAsync(
                chatId: chatId.Value,
                text: "⏳ Searching for the nearest parking spot...",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);

            try
            {
                var intent = await new IntentRecognitionService(
                        new DeepSeekService(_deepSeekSettings.ApiKey!, _deepSeekSettings.BaseUrl,
                            _deepSeekSettings.Model))
                    .RecognizeIntentAsync(messageText);

                _logger.LogInformation("Recognized intent for user {UserId}: {Intent}",
                    Context.GetSafeUserId(), intent);

                var result = await new ParkingActionService().ExecuteActionAsync(intent);

                if (result.DateTimeFrom != null && result.DateTimeTo != null &&
                    !string.IsNullOrEmpty(result.Address) && !string.IsNullOrEmpty(result.CarNumber))
                {
                    var baseUrl = _geoAdminSettings.GeoAdminBaseUrl;
                    var labels = (await _geoAdminSearcher.GetAddress(baseUrl, result.Address)).ToList();

                    var sessionState = new BotSessionState { MessageId = progressMsg.MessageId };

                    if (labels.Count == 0)
                    {
                        _logger.LogWarning("AI flow found no nearby addresses for query '{Address}'", result.Address);
                        await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
                        var msg = await Send("No nearby addresses found 😔");
                        sessionState.MessageId = msg.MessageId;
                        return;
                    }

                    var firstAddress = labels.First();

                    var spaces = await _spaceApi.GetNearestSpacesAsync(
                        firstAddress.lat, firstAddress.lon, spaceTypeId: 1,
                        maxDistanceMeters: 100000, count: 7);

                    if (spaces.Count == 0)
                    {
                        _logger.LogWarning("AI flow found no nearby spaces for address '{Address}'", result.Address);
                        await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
                        var msg = await Send("No nearby spaces found 😔");
                        sessionState.MessageId = msg.MessageId;
                        return;
                    }

                    var firstSpace = spaces.First();
                    var parkingType = string.Empty;
                    if (!firstSpace.SpaceProperties.IsEmptyJson() && firstSpace.SpaceProperties != null)
                    {
                        var node = JsonNode.Parse(firstSpace.SpaceProperties);
                        parkingType = node?["Type"]?.GetValue<string>() ?? string.Empty;
                    }

                    var geoAddressInputToNearestPoint = new GeoAddressInput
                    {
                        FeatureId = firstAddress.featureId,
                        NormalizedAddress = firstAddress.label,
                        OriginalInput = new Point(firstAddress.lat, firstAddress.lon),
                        Location = new Point(firstSpace.Latitude!.Value, firstSpace.Longitude!.Value),
                        Type = parkingType
                    };

                    sessionState.CurrentReservation = new ReservationDraft
                    {
                        SelectedSpaceId = firstSpace.Id,
                        SelectedSpaceTypeId = firstSpace.SpaceTypeId,
                        PendingStartRentLocal = result.DateTimeFrom,
                        PendingEndRentLocal = result.DateTimeTo,
                        SpaceTimeZone = firstSpace.TimeZoneId,
                        SelectedCarNumber = result.CarNumber,
                        GeoAddressInput = geoAddressInputToNearestPoint
                    };

                    _logger.LogInformation("AI flow produced complete reservation data for user {UserId}",
                        Context.GetSafeUserId());

                    await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
                    await Call<MainController>(m => m.ShowReservationSummary(sessionState, result.CarNumber));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AI flow for user {UserId}", Context.GetSafeUserId());
                await Send("⚠️ AI service error. Please try again later.");
            }
        }
    }
}