using System.Globalization;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;
using AlgoTecture.GeoAdminSearch;
using AlgoTecture.TelegramBot.Api.Controllers.Base;
using AlgoTecture.TelegramBot.Application.Helpers;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Domain;
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
    private readonly ISpaceServiceClient _spaceClient;

    public MainController(IUserAuthenticationService authService, IReservationFlowService flow,
        IGeoAdminSearcher geoAdminSearcher, IOptions<GeoAdminSettings> options, ISpaceServiceClient spaceClient)
        : base(flow)
    {
        _authService = authService;
        _geoAdminSearcher = geoAdminSearcher;
        _spaceClient = spaceClient;
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

        // var linkedUserId = await _authService.EnsureUserAuthenticatedAsync(
        //     userId.Value,
        //     chatId!.Value,
        //     userFullName,
        //     userName
        // );
        // if (linkedUserId == Guid.Empty) return;
        //Idustriestrasse 24 8305
        //Thread.Sleep(100000);

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
            await Send("Choose the right address");
        }
    }

    [Action]
    public async Task ShowNearestSpaces(BotSessionState sessionState, GeoAddressInput geoAddressInput)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;


        var spaces = await _spaceClient.GetNearestByTypeAsync(geoAddressInput.OriginalInput.X,
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
            };
            
            int? roundedDistanceInMeters =
                space.DistanceMeters.HasValue ? (int)Math.Round(space.DistanceMeters.Value) : null;

            RowButton($"{parkingType} parking — {roundedDistanceInMeters} m", Q(ShowDetails, sessionState, geoAddressInputToNearestPoint));
        }

        RowButton("↩️ go back", Q(EnterAddress, sessionState));

        PushL("🅿️ Available parking");
        await SendOrUpdate();
    }

    [Action]
    public async Task ShowDetails(BotSessionState sessionState, GeoAddressInput geoAddressInputToNearestPoint)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;


        await Client.SendLocationAsync(
            chatId: chatId.Value,
            latitude: (float)geoAddressInputToNearestPoint.Location.Y,
            longitude: (float)geoAddressInputToNearestPoint.Location.X,
            replyMarkup: new InlineKeyboardMarkup([
                [
                    InlineKeyboardButton.WithCallbackData("🅿️ make a reservation", 
                    Q(ShowNearestSpaces, sessionState, geoAddressInputToNearestPoint))
                ],

                [
                    InlineKeyboardButton.WithCallbackData("↩️ go back", 
                    Q(ShowNearestSpaces, sessionState, geoAddressInputToNearestPoint))
                ]
            ])
        );

        PushL("Here is the parking spot 📍");
        await SendOrUpdate();
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