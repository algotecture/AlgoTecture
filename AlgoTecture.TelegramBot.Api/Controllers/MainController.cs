using System.Text.Json.Nodes;
using AlgoTecture.GeoAdminSearch;
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

    public MainController(IUserAuthenticationService authService, IReservationFlowService flow,
        IGeoAdminSearcher geoAdminSearcher, IOptions<GeoAdminSettings> options, IUserCarsApi userCarsApi, IUserCache cache, ISpaceApi spaceApi)
        : base(flow)
    {
        _authService = authService;
        _geoAdminSearcher = geoAdminSearcher;
        _userCarsApi = userCarsApi;
        _cache = cache;
        _spaceApi = spaceApi;
        _geoAdminSettings = options.Value;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var userFullName = Context.GetUserFullName();
        if (userId == null) return;

        var linkedUserId = await _authService.EnsureUserAuthenticatedAsync(
            userId.Value,
            chatId!.Value,
            userFullName,
            userName
        );
        if (linkedUserId == Guid.Empty) return;
        //Idustriestrasse 24 8305
        // Thread.Sleep(100000);

        PushL("I am your parking 🅿️ assistant. I help you find and manage spots near you.");

        BotSessionState state = new BotSessionState
            { CurrentReservation = new ReservationDraft { SelectedSpaceTypeId = 1 } };

        RowButton("🚗 reserve a parking", Q(SelectRentalTime, state, TimeSelectionStage.None, null!));
        //RowButton("📅 manage reservations", Q(PressToFindReservationsButton));
    }

    [Action]
    public async Task SelectRentalTime(BotSessionState sessionState, TimeSelectionStage stage, DateTime? dateTime)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var time = string.Empty;
        if (dateTime != null)
        {
            PushL("Enter the rental start time (in HH:mm format, for example, 14:15)");
            if (sessionState.MessageId == 0)
                await Send();
            else
            {
                var message = await SendOrUpdate();
                sessionState.MessageId = message!.MessageId;
            }

            time = await AwaitText(() => Send("Text input timeout. Use /start to try again"));

            await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
        }

        sessionState.CurrentReservation.PendingStartRentLocal = stage == TimeSelectionStage.Start
            ? DateTimeParser.GetLocalDateTime(dateTime, time)
            : sessionState.CurrentReservation.PendingStartRentLocal;
        sessionState.CurrentReservation.PendingEndRentLocal = stage == TimeSelectionStage.End
            ? DateTimeParser.GetLocalDateTime(dateTime, time)
            : sessionState.CurrentReservation.PendingEndRentLocal;

        if (sessionState.CurrentReservation.PendingEndRentLocal != null &&
            sessionState.CurrentReservation.PendingEndRentLocal <= DateTimeOffset.UtcNow)
            sessionState.CurrentReservation.PendingEndRentLocal = null;

        if (sessionState.CurrentReservation.PendingStartRentLocal != null &&
            sessionState.CurrentReservation.PendingStartRentLocal <= DateTimeOffset.UtcNow)
            sessionState.CurrentReservation.PendingStartRentLocal = null;

        if (sessionState.CurrentReservation.PendingStartRentLocal != null &&
            sessionState.CurrentReservation.PendingEndRentLocal != null &&
            sessionState.CurrentReservation.PendingEndRentLocal <=
            sessionState.CurrentReservation.PendingStartRentLocal)
            sessionState.CurrentReservation.PendingEndRentLocal = null;

        RowButton(sessionState.CurrentReservation.PendingStartRentLocal != null
            ? $"{sessionState.CurrentReservation.PendingStartRentLocal:dddd, MMMM dd yyyy HH:mm}"
            : "⏱️ start time", Q(PressToChooseTheDate, sessionState, TimeSelectionStage.Start));
        RowButton(sessionState.CurrentReservation.PendingEndRentLocal != null
            ? $"{sessionState.CurrentReservation.PendingEndRentLocal:dddd, MMMM dd yyyy HH:mm}"
            : "end time⏱️", Q(PressToChooseTheDate, sessionState, TimeSelectionStage.End));

        if (sessionState.CurrentReservation.PendingStartRentLocal != null &&
            sessionState.CurrentReservation.PendingEndRentLocal != null)
        {
            RowButton("📍 where to park?", Q(EnterAddress, sessionState));
        }

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
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        PushL("Enter the address or part of the address (or type 'back' to return)");
        var message = await SendOrUpdate();
        sessionState.MessageId = message!.MessageId;

        var address = await AwaitText(() => Send("Text input timeout. Use /start to try again"));

        if (address.Equals("back", StringComparison.OrdinalIgnoreCase))
        {
            sessionState.MessageId = 0;
            await Call<MainController>(m => m.SelectRentalTime(sessionState, TimeSelectionStage.None, null!));
            return;
        }

        await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);

        var geoAddressInputList = new List<GeoAddressInput>();

        var baseUrlToAdminSearcher = _geoAdminSettings.GeoAdminBaseUrl;

        var labels = (await _geoAdminSearcher.GetAddress(baseUrlToAdminSearcher, address)).ToList();

        foreach (var label in labels)
        {
            var geoAddressInput = new GeoAddressInput
            {
                FeatureId = label.featureId,
                NormalizedAddress = label.label,
                OriginalInput = new Point(label.lat, label.lon)
            };
            geoAddressInputList.Add(geoAddressInput);

            RowButton(label.label,
                Q(ShowNearestSpaces, sessionState, geoAddressInput));
        }

        RowButton("↩️ go back", Q(SelectRentalTime, sessionState, TimeSelectionStage.None, null!));

        if (!labels.Any())
        {
            RowButton("Try again"!);
            await SendOrUpdate();
        }
        else
        {
            var msg = await Send("Choose the right address");
            sessionState.MessageId = msg!.MessageId;
        }
    }

    [Action]
    public async Task ShowNearestSpaces(BotSessionState sessionState, GeoAddressInput geoAddressInput)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        
        var spaces = await _spaceApi.GetNearestSpacesAsync(geoAddressInput.OriginalInput.X,
            geoAddressInput.OriginalInput.Y, spaceTypeId: 1,
            maxDistanceMeters: 100000, count: 7);

        if (spaces.Count == 0)
        {
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

            int? roundedDistanceInMeters =
                space.DistanceMeters.HasValue ? (int)Math.Round(space.DistanceMeters.Value) : null;

            RowButton($"{parkingType} parking — {roundedDistanceInMeters} m",
                Q(ShowDetails, sessionState, geoAddressInputToNearestPoint));
        }

        RowButton("↩️ go back", Q(EnterAddress, sessionState));

        PushL("🅿️ Available parking");
        
        await DeletePreviousMessageIfNeeded(sessionState, chatId!.Value);
        await DeletePreviousLocationMessageIfNeeded(sessionState, chatId!.Value);
        
        var textMessage = await Send();
        sessionState.MessageId = textMessage!.MessageId;
    }

    [Action]
    public async Task ShowDetails(BotSessionState sessionState, GeoAddressInput geoAddressInputToNearestPoint)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        await DeletePreviousMessageIfNeeded(sessionState, chatId!.Value);
        var locationMessage = await Client.SendLocationAsync(
            chatId: chatId.Value,
            latitude: (float)geoAddressInputToNearestPoint.Location.Y,
            longitude: (float)geoAddressInputToNearestPoint.Location.X,
            replyMarkup: new InlineKeyboardMarkup([
                [
                    InlineKeyboardButton.WithCallbackData("🅿️ make a reservation",
                        Q(MakeAReservation, sessionState, geoAddressInputToNearestPoint))
                ],

                [
                    InlineKeyboardButton.WithCallbackData("↩️ go back",
                        Q(ShowNearestSpaces, sessionState, geoAddressInputToNearestPoint))
                ]
            ])
        );

        sessionState.LocationMessageId = locationMessage.MessageId;

        PushL("Here is the parking spot 📍");
        var textMessage = await SendOrUpdate();
        sessionState.MessageId = textMessage!.MessageId;
    }

    [Action]
    public async Task MakeAReservation(BotSessionState sessionState, GeoAddressInput geo)
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
        await DeletePreviousLocationMessageIfNeeded(sessionState, chatId!.Value);

        if (userCarsDto.CarNumbers.Count == 0)
        {
            PushL("You have no car numbers yet 🚗");
            
            var message = await Send();
            sessionState.MessageId = message!.MessageId;
            await Client.SendTextMessageAsync(chatId!.Value,
                "Add your first car number:",
                replyMarkup: new InlineKeyboardMarkup([
                    [InlineKeyboardButton.WithCallbackData("➕ add car number", Q(AddCarNumber, sessionState, geo)), 
                        InlineKeyboardButton.WithCallbackData("↩️ go back", Q(ShowDetails, sessionState, geo))]
                ]));
            return;
        }

        var buttons = userCarsDto.CarNumbers
            .Select(curNumber => new[]
                { InlineKeyboardButton.WithCallbackData($"🚘 {curNumber}", Q(ShowReservationSummary, sessionState, geo, curNumber)) })
            .ToList();

        buttons.Add([InlineKeyboardButton.WithCallbackData("➕ add new", Q(AddCarNumber, sessionState, geo))]);
        
        buttons.Add([InlineKeyboardButton.WithCallbackData("↩️ go back", Q(ShowDetails, sessionState, geo))]);

        var msg = await Client.SendTextMessageAsync(chatId.Value,
            "Select to confirm a car number for reservation:",
            replyMarkup: new InlineKeyboardMarkup(buttons));
        sessionState.MessageId = msg.MessageId;
    }

    [Action]
    public async Task AddCarNumber(BotSessionState sessionState, GeoAddressInput geo)
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
        
        var userCarsDto = await _userCarsApi.AddCarNumberAsync(cachedLinkedUserId!.Value, new AddCarNumberRequest(input));

        PushL($"✅ Added: {input}. You now have {userCarsDto.CarNumbers.Count} car(s).");
        await SendOrUpdate();
        
        await Call<MainController>(m => m.MakeAReservation(sessionState, geo));
    }
    
    [Action]
    public async Task ShowReservationSummary(BotSessionState sessionState, GeoAddressInput geo, string carNumber)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        
        var draft = sessionState.CurrentReservation;
        if (draft.PendingStartRentLocal == null || draft.PendingEndRentLocal == null)
        {
            await Send("⚠️ Missing reservation data. Please start over with /start");
            return;
        }

        var summaryText = $"""
                           🧾 <b>Reservation summary</b>

                           📍 <b>Address:</b> {geo.NormalizedAddress}
                           🚗 <b>Car:</b> {carNumber}
                           🕒 <b>From:</b> {draft.PendingStartRentLocal:dd.MM.yyyy HH:mm}
                           🕒 <b>To:</b> {draft.PendingEndRentLocal:dd.MM.yyyy HH:mm}
                           🏷 <b>Space type:</b> {geo.Type}
                           💰 <b>Price:</b> will be calculated after confirmation
                           """;

        var markup = new InlineKeyboardMarkup([
            [
                InlineKeyboardButton.WithCallbackData("✅ confirm", Q(ConfirmReservation, sessionState, geo, carNumber))
            ],
            [
                InlineKeyboardButton.WithCallbackData("↩️ go back", Q(MakeAReservation, sessionState, geo))
            ]
        ]);

        await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);
        await DeletePreviousLocationMessageIfNeeded(sessionState, chatId.Value);

        var message = await Client.SendTextMessageAsync(
            chatId: chatId.Value,
            text: summaryText,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
            replyMarkup: markup
        );

        sessionState.MessageId = message.MessageId;
    }

    [Action]
    public async Task ConfirmReservation(BotSessionState sessionState, GeoAddressInput geo, string carNumber)
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        if (userId == null || !chatId.HasValue) return;

        var linkedUserId = await _cache.GetUserIdByTelegramAsync(userId.Value);
        if (linkedUserId == Guid.Empty)
        {
            await Send("⚠️ Session expired. Please /start again.");
            return;
        }

        var draft = sessionState.CurrentReservation;
        if (draft.PendingStartRentLocal == null || draft.PendingEndRentLocal == null)
        {
            await Send("⚠️ Missing reservation data.");
            return;
        }

//         var command = new CreateReservationCommand
//         {
//             UserId = linkedUserId,
//             SpaceId = draft.SpaceId ?? Guid.Empty,
//             Address = geo.NormalizedAddress,
//             Start = draft.PendingStartRentLocal.Value,
//             End = draft.PendingEndRentLocal.Value,
//             CarNumber = carNumber
//         };
//
//         var result = await _flow.CreateReservationAsync(command);
//
          var confirmation = $"""
                              ✅ <b>Reservation confirmed!</b>

                              📍 {geo.NormalizedAddress}
                              🚗 {carNumber}
                              🕒 {draft.PendingStartRentLocal:dd.MM.yyyy HH:mm} – {draft.PendingEndRentLocal:dd.MM.yyyy HH:mm}
                              🆔 Reservation ID: 
                              """;

        await DeletePreviousMessageIfNeeded(sessionState, chatId.Value);

        var message = await Client.SendTextMessageAsync(
            chatId: chatId.Value,
            text: confirmation,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
        );

        sessionState.MessageId = message.MessageId;
    }

    [On(Handle.Exception)]
    public void Exception(Exception ex)
    {
        //_logger.LogError(ex, "Handle.Exception on telegram-bot");
    }

    [On(Handle.ChainTimeout)]
    void ChainTimeout(Exception ex)
    {
        //_logger.LogError(ex, "Handle.Exception on telegram-bot");
    }
}