using System.Globalization;
using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Domain.Enum;
using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.GeoAdminSearch;
using Algotecture.Libraries.PriceSpecifications;
using Algotecture.Libraries.Reservations;
using Algotecture.Libraries.Reservations.Models;
using Algotecture.Libraries.Spaces.Interfaces;
using Algotecture.TelegramBot.Controllers;
using Algotecture.TelegramBot.Controllers.Interfaces;

using Algotecture.TelegramBot.Implementations;

using Algotecture.TelegramBot.Interfaces;

using Algotecture.TelegramBot.Models;

using Deployf.Botf;
using Telegram.Bot;

namespace AlgoTecture.TelegramBot.Controllers;

public class ParkingController : BotController, IParkingController
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPriceCalculator _priceCalculator;
    private readonly ILogger<MainController> _logger;
    private readonly IGeoAdminSearcher _geoAdminSearcher;
    private readonly ITelegramToAddressResolver _telegramToAddressResolver;
    private readonly ISpaceGetter _spaceGetter;
    private readonly ISpaceService _spaceService;
    private readonly IReservationService _reservationService;

    public ParkingController(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IPriceCalculator priceCalculator, ILogger<MainController> logger, IGeoAdminSearcher geoAdminSearcher, ITelegramToAddressResolver telegramToAddressResolver, ISpaceGetter spaceGetter, ISpaceService spaceService, IReservationService reservationService)
    {
        _serviceProvider = serviceProvider;
        _unitOfWork = unitOfWork;
        _priceCalculator = priceCalculator;
        _logger = logger;
        _geoAdminSearcher = geoAdminSearcher;
        _telegramToAddressResolver = telegramToAddressResolver;
        _spaceGetter = spaceGetter;
        _spaceService = spaceService;
        _reservationService = reservationService;
    }

    [Action]
    public async Task PressToMainBookingPage(BotState botState)
    {
        //only for demo
        const int parkingTargetOfSpaceId = 11;
        if (botState.UtilizationTypeId != parkingTargetOfSpaceId)
        {
            return;
        }
        
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();

        RowButton("Enter address", Q(EnterAddress, new BotState{UtilizationTypeId = 11}));
        RowButton("Go Back", Q(mainControllerService.PressToRentButton));
        
        PushL("Enter an address to search for the parking space");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task EnterAddress(BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
         
        PushL("Enter the address or part of the address");
        await SendOrUpdate();
         
        var address = await AwaitText(() => Send("Text input timeout. Use /start to try again"));

        var user = await _unitOfWork.TelegramUserInfos.GetByTelegramChatId(chatId.Value);
         
        _logger.LogInformation($"User {user?.TelegramUserFullName} entered text {address} to search for an address");
        
        var telegramToAddressList = new List<TelegramToAddressModel>();

        var labels = (await _geoAdminSearcher.GetAddress(address)).ToList();
        
        foreach (var label in labels)
        {
            var telegramToAddressModel = new TelegramToAddressModel
            {
                FeatureId = label.featureId,
                OriginalAddressLatitude = label.lat,
                OriginalAddressLongitude = label.lon,
                Address = label.label
            };
            telegramToAddressList.Add(telegramToAddressModel);

            RowButton(label.label,
                Q(PressAddressToRentButton, telegramToAddressModel,
                    new BotState() { UtilizationTypeId = botState.UtilizationTypeId, SpaceAddress = label.label }));
        }

        if (!labels.Any())
        {
            RowButton("Try again"!);
            await Send("Nothing found");
        }
        else
        {
            _telegramToAddressResolver.TryAddCurrentAddressList(chatId.Value, telegramToAddressList);

            if (_telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value)!.Count > 1)
            {
                await Send("Choose the right address");   
            }
            else
            {
                await Send("Address");   
            }
        }
    }
    
     [Action]
     private async Task PressAddressToRentButton(TelegramToAddressModel telegramToAddressModel, BotState botState)
     {
         var chatId = Context.GetSafeChatId();
         if (!chatId.HasValue) return;
         
        //only for demo
        if (botState.UtilizationTypeId == 11)
        {
            //only for demo 
            var targetSpaces = await _spaceGetter.GetByType(botState.UtilizationTypeId);
            
            var nearestParkingSpaces = await _spaceService.GetNearestSpaces(targetSpaces, 
                telegramToAddressModel.OriginalAddressLatitude, telegramToAddressModel.OriginalAddressLongitude, 7);

            if (nearestParkingSpaces.Any())
            {
                var parkingControllerService = _serviceProvider.GetRequiredService<IParkingController>();
                var counter = 1;
                foreach (var nearestParkingSpace in nearestParkingSpaces)
                {
                    var tamModel = new TelegramToAddressModel
                    {
                        latitude = nearestParkingSpace.Value.Latitude,
                        longitude = nearestParkingSpace.Value.Longitude
                    };
                    //
                    RowButton($"{counter}. In {nearestParkingSpace.Key} meters. Tap to details",
                        Q(parkingControllerService.PressToParkingButton, tamModel,
                            new BotState
                            {
                                UtilizationTypeId = botState.UtilizationTypeId, SpaceId = nearestParkingSpace.Value.Id, SpaceAddress =botState.SpaceAddress
                            }));
                    counter++;
                }  
                var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();
                RowButton("Go Back", Q(mainControllerService.PressToRentButton));
         
                PushL($"Found!");
            }
            else
            {
                RowButton("Try again"!);
                await Send("Nothing found"); 
            }
        }
     }
    
    [Action]
    public async Task PressToParkingButton(TelegramToAddressModel telegramToAddressModel, BotState botState)
    {
        //for example
        //var urlToAddressProperties = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={telegramToAddressModel.latitude.ToString(CultureInfo.InvariantCulture)},{telegramToAddressModel.longitude.ToString(CultureInfo.InvariantCulture)}&key=.....&language=en";

        var latitude = telegramToAddressModel.latitude.ToString(CultureInfo.InvariantCulture);
        var longitude = telegramToAddressModel.longitude.ToString(CultureInfo.InvariantCulture);
        
        var urlToAddressProperties = $"https://www.google.com/maps/search/?api=1&query={latitude},{longitude}";
        RowButton("Look on the map", urlToAddressProperties);
        RowButton("Make a reservation", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null!));
        RowButton("Go Back", Q(EnterAddress, botState));

        PushL("Details");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressToEnterTheStartEndTime(BotState botState, RentTimeState rentTimeState, DateTime? dateTime)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        
        if (botState.MessageId != default)
        {
            await Client.DeleteMessageAsync(chatId, botState.MessageId);
            botState.MessageId = default;
        }

        var time = string.Empty;
        if (dateTime != null)
        {
            PushL("Enter the rental start time (in HH:mm format, for example, 14:15)");
            await Send();
            time = await AwaitText(() => Send("Text input timeout. Use /start to try again"));
        }
    
        botState.StartRent = rentTimeState == RentTimeState.StartRent ? DateTimeParser.GetDateTimeUtc(dateTime, time) : botState.StartRent;
        botState.EndRent = rentTimeState == RentTimeState.EndRent ? DateTimeParser.GetDateTimeUtc(dateTime, time) : botState.EndRent;

        //for demo. timezone needed
        if (botState.EndRent != null && botState.EndRent <= DateTime.UtcNow)
        {
            botState.EndRent = null;
        }

        if (botState.StartRent != null && botState.StartRent <= DateTime.UtcNow)
        {
            botState.StartRent = null;
        }
        
        if (botState.EndRent != null && botState.StartRent != null && botState.EndRent <= botState.StartRent)
        {
            botState.EndRent = null;
        }
        //only for demo utc +2
        var startTimeToDemo = botState.StartRent.HasValue ? botState.StartRent.Value + TimeSpan.FromHours(2) : DateTime.UtcNow;
        var endTimeToDemo = botState.EndRent.HasValue ? botState.EndRent.Value + TimeSpan.FromHours(2) : DateTime.UtcNow;
        RowButton(botState.StartRent != null ? $"{startTimeToDemo:dddd, MMMM dd yyyy HH:mm}"
                : "Rental start time", Q(PressToChooseTheDate, botState, RentTimeState.StartRent));
        RowButton(botState.EndRent != null ? $"{endTimeToDemo:dddd, MMMM dd yyyy HH:mm}"
                : "Rental end time", Q(PressToChooseTheDate, botState, RentTimeState.EndRent));
            //
        if (botState.StartRent != null && botState.EndRent != null && botState.SpaceId != default)
        {
            var targetPriceSpecification = new PriceSpecification { PricePerTime = 2.ToString(), PriceCurrency = "CHF"};
              // (await _unitOfWork.PriceSpecifications.GetBySpaceId(botState.SpaceId)).FirstOrDefault(x => x.UnitOfTime == UnitOfDateTime.Hour.ToString());
        
            if (targetPriceSpecification == null) throw new ArgumentNullException("No price specification");
        
            var totalPrice = _priceCalculator.CalculateTotalPriceToReservation(botState.StartRent.Value, botState.EndRent.Value,
                UnitOfDateTime.Hour, targetPriceSpecification.PricePerTime);
            
            RowButton($"Make a reservation! {totalPrice} {targetPriceSpecification.PriceCurrency}", Q(PressMakeAReservation, botState));   
        }
        
        RowButton("Go Back", Q(EnterAddress, botState));

        if (string.IsNullOrEmpty(time))
        {
            PushL("Parking reservation");
            await SendOrUpdate();   
        }
        else
        {
            await Send("Parking reservation"); 
        }
    }
    
    [Action]
    private async Task PressMakeAReservation(BotState botState)
    {
        if (botState.StartRent == null) throw new ArgumentNullException(nameof(botState.StartRent));
        if (botState.EndRent == null) throw new ArgumentNullException(nameof(botState.EndRent));
        
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        
        try
        {
            var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
            //var targetPriceSpecification = (await _unitOfWork.PriceSpecifications.GetBySpaceId(botState.SpaceId)).FirstOrDefault();
            //if (targetPriceSpecification == null) return;

            var addReservationModel = new AddReservationModel
            {
                TenantUserId = user.Id,
                SpaceId = botState.SpaceId,
                ReservationDateTimeUtc = DateTime.UtcNow,
                ReservationFromUtc = botState.StartRent.Value,
                ReservationToUtc = botState.EndRent.Value,
                Description = string.IsNullOrEmpty(botState.SpaceName) ? botState.SpaceAddress : botState.SpaceName,
            };
            var checkedReservation = await _reservationService.CheckReservation(botState.SpaceId, null!, botState.StartRent.Value,
                botState.EndRent.Value);
            if (checkedReservation.Any())
            {
               _logger.LogInformation($"User {user.TelegramUserInfo?.TelegramUserFullName} tried to reserve a space {botState.SpaceName} with id " +
                                      $"{botState.SpaceId}. But this time is already reserved");

               PushL("This time is already reserved");
               RowButton("Go to reservation and try again", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null!));
               await SendOrUpdate();
            }
            else
            {
                var reservation = await _reservationService.AddReservation(addReservationModel, 2.ToString());

                if (reservation != null)
                {
                    var spaceAddress = botState.SpaceAddress;
                        //(await _unitOfWork.Spaces.GetById(addReservationModel.SpaceId))?.SpaceAddress;
                    
                    var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();
                    RowButton("Go to my reservations", Q(mainControllerService.PressToFindReservationsButton));

                    _logger.LogInformation($"User {user.TelegramUserInfo?.TelegramUserFullName} reserved parking {botState.SpaceId} from " +
                                           $"{botState.StartRent.Value:dddd, MMMM dd yyyy HH:mm} to {botState.EndRent.Value:dddd, MMMM dd yyyy HH:mm} by telegram bot. " +
                                           $"ReservationId: {reservation.Id}");
                    //only for demo utc +2
                    var startTimeToDemo = botState.StartRent.Value + TimeSpan.FromHours(2);
                    var endTimeToDemo = botState.EndRent.Value + TimeSpan.FromHours(2);
                    PushL("🎉 Congratulations! Your space reservation has been successfully confirmed. " +
                          "You're all set to enjoy your reserved space. Please find the details below: \n\r \n\r" +
                          $"📅 Date: {startTimeToDemo:dddd, MMMM dd}\n\r" +
                          $"⌚ Time: {startTimeToDemo:HH:mm} to {endTimeToDemo:HH:mm}\n\r"  +
                          $"📍 Location: {spaceAddress}\n\r" +
                          $"🔢 Confirmation Number: {reservation.ReservationUniqueIdentifier}\n\r \n\r" +
                          "If you have any questions or need to make changes to your reservation, " +
                          "please feel free to contact our support team at @AlgoTecture." +
                          " Thank you for choosing our service! 🙌");

                    await SendOrUpdate();
                }   
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [Action]
    private async Task PressToChooseTheDate(BotState botState, RentTimeState rentTimeState)
    {
        await Calendar("", botState, false, rentTimeState);
    }
    
    [Action]
    private async Task Calendar(string state, BotState botState, bool isNavigateBetweenMonths, RentTimeState rentTimeState)
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
                .OnNavigatePath(s => Q(Calendar, s, botState, true, rentTimeState))
                .OnSelectPath(date =>
                    Q(PressToEnterTheStartEndTime, botState, rentTimeState, date));
        }
        else
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, botState, true, rentTimeState))
                .OnSelectPath(date =>
                    Q(PressToEnterTheStartEndTime, botState, rentTimeState, date))
                .SkipDay(d => d.Day < now.Day);
        }

        calendar.Build(Message, new PagingService());

        RowButton("Go Back", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null!));
        PushL("Pick the date");
        await SendOrUpdate();
    }
    
    
}