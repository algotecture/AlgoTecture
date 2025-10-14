using AlgoTecture.GeoAdminSearch;
using AlgoTecture.TelegramBot.Api.Controllers.Base;
using AlgoTecture.TelegramBot.Application.Helpers;
using AlgoTecture.TelegramBot.Application.Models;
using AlgoTecture.TelegramBot.Application.Services;
    using AlgoTecture.TelegramBot.Domain;
using Deployf.Botf;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class MainController : ReservationControllerBase
{
    private readonly TimeZoneInfo _zurichTz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Zurich");

    private readonly IUserAuthenticationService _authService;
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    private readonly GeoAdminSettings _geoAdminSettings;

    public MainController(IUserAuthenticationService authService, IReservationFlowService flow, IGeoAdminSearcher geoAdminSearcher, IOptions<GeoAdminSettings> options)
        : base(flow)
    {
        _authService = authService;
        _geoAdminSearcher = geoAdminSearcher;
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

        sessionState.CurrentReservation.PendingStartRentUtc = stage == TimeSelectionStage.Start
            ? DateTimeParser.GetDateTimeUtc(dateTime, time)
            : sessionState.CurrentReservation.PendingStartRentUtc;
        sessionState.CurrentReservation.PendingEndRentUtc = stage == TimeSelectionStage.End
            ? DateTimeParser.GetDateTimeUtc(dateTime, time)
            : sessionState.CurrentReservation.PendingEndRentUtc;

         if (sessionState.CurrentReservation.PendingEndRentUtc != null && sessionState.CurrentReservation.PendingEndRentUtc <= DateTime.UtcNow)
             sessionState.CurrentReservation.PendingEndRentUtc = null;

        if (sessionState.CurrentReservation.PendingStartRentUtc != null && sessionState.CurrentReservation.PendingStartRentUtc <= DateTime.UtcNow)
            sessionState.CurrentReservation.PendingStartRentUtc = null;

        if (sessionState.CurrentReservation.PendingStartRentUtc != null && sessionState.CurrentReservation.PendingEndRentUtc!= null &&
            sessionState.CurrentReservation.PendingEndRentUtc <= sessionState.CurrentReservation.PendingStartRentUtc)
            sessionState.CurrentReservation.PendingEndRentUtc = null;
        
        
        DateTime? startTimeToZurichTz = sessionState.CurrentReservation.PendingStartRentUtc.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(sessionState.CurrentReservation.PendingStartRentUtc.Value, _zurichTz)
            : null;

        DateTime? endTimeToZurichTz = sessionState.CurrentReservation.PendingEndRentUtc.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(sessionState.CurrentReservation.PendingEndRentUtc.Value, _zurichTz)
            : null;

        RowButton(sessionState.CurrentReservation.PendingStartRentUtc != null ? $"{startTimeToZurichTz:dddd, MMMM dd yyyy HH:mm}"
                : "⏱️ start time", Q(PressToChooseTheDate, sessionState, TimeSelectionStage.Start));
        RowButton(sessionState.CurrentReservation.PendingEndRentUtc != null ? $"{endTimeToZurichTz:dddd, MMMM dd yyyy HH:mm}"
                : "end time⏱️", Q(PressToChooseTheDate, sessionState, TimeSelectionStage.End));
            //
        if (sessionState.CurrentReservation.PendingStartRentUtc != null && sessionState.CurrentReservation.PendingEndRentUtc != null)
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
    private async Task Calendar(string state, BotSessionState sessionState, bool isNavigateBetweenMonths, TimeSelectionStage stage)
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
        await SendOrUpdate();
    }
    
    [Action]
    public async Task EnterAddress(BotSessionState sessionState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
         
        PushL("Enter the address or part of the address (or type 'back' to return)");
        var message = await SendOrUpdate();
        sessionState.MessageId = message.MessageId;
         
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
                Location = new Point(label.lat, label.lon),
                NormalizedAddress = label.label
            };
            geoAddressInputList.Add(geoAddressInput);

            RowButton(label.label,
                Q(PressAddressToRentButton));
        }

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
     private async Task PressAddressToRentButton()
     {
        //  var chatId = Context.GetSafeChatId();
        //  if (!chatId.HasValue) return;
        //  
        // //only for demo
        // if (botState.UtilizationTypeId == 15 || botState.UtilizationTypeId == 16)
        // {
        //     //only for demo 
        //     var targetSpacesInside = await _spaceGetter.GetByType(15);
        //     var targetSpacesOutside = await _spaceGetter.GetByType(16);
        //     var targetSpaces = targetSpacesInside.Take(1).ToList();
        //     targetSpaces.AddRange(targetSpacesOutside);
        //     
        //     var nearestParkingSpaces = await _spaceService.GetNearestSpaces(targetSpaces, 
        //         Convert.ToDouble(telegramToAddressModel.OriginalAddressLatitude, CultureInfo.InvariantCulture), Convert.ToDouble(telegramToAddressModel.OriginalAddressLongitude, CultureInfo.InvariantCulture), 7);
        //
        //     if (nearestParkingSpaces.Any())
        //     {
        //         var counter = 1;
        //         foreach (var nearestParkingSpace in nearestParkingSpaces)
        //         {
        //             var tamModel = new TelegramToAddressModel
        //             {
        //                 latitude = nearestParkingSpace.Value.Latitude.ToString(CultureInfo.InvariantCulture),
        //                 longitude = nearestParkingSpace.Value.Longitude.ToString(CultureInfo.InvariantCulture),
        //                 OriginalAddressLatitude = telegramToAddressModel.OriginalAddressLatitude,
        //                 OriginalAddressLongitude = telegramToAddressModel.OriginalAddressLongitude,
        //             };
        //             //
        //             if (nearestParkingSpace.Value.UtilizationTypeId == 15)
        //             {
        //                 RowButton($"Underground. In {nearestParkingSpace.Key} meters. Tap to details",
        //                     Q(PressToParkingButton, tamModel,
        //                         new BotState
        //                         {
        //                             UtilizationTypeId = nearestParkingSpace.Value.UtilizationTypeId, SpaceId = nearestParkingSpace.Value.Id, SpaceAddress =botState.SpaceAddress,
        //                             StartRent = botState.StartRent, EndRent = botState.EndRent
        //                         }));
        //                 counter++;
        //             }
        //             else
        //             {
        //                 RowButton($"Street. In {nearestParkingSpace.Key} meters. Tap to details",
        //                     Q(PressToParkingButton, tamModel,
        //                         new BotState
        //                         {
        //                             UtilizationTypeId = nearestParkingSpace.Value.UtilizationTypeId, SpaceId = nearestParkingSpace.Value.Id, SpaceAddress =botState.SpaceAddress,
        //                             StartRent = botState.StartRent, EndRent = botState.EndRent
        //                         }));
        //                 counter++;  
        //             }
        //         }  
        //         RowButton("↩️ go back", Q(EnterAddress, botState));
        //  
        //         PushL($"Found!");
        //         await SendOrUpdate(); 
        //     }
        //     else
        //     {
        //         RowButton("Try again"!);
        //         await Send("Nothing found"); 
        //     }
        // }
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