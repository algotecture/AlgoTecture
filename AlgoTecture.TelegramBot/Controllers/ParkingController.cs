using System.Globalization;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Enum;
using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Environments;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.PriceSpecifications;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Reservations.Models;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

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
    private readonly TimeZoneInfo _zurichTz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Zurich");

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
    public async Task EnterAddress(BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
         
        PushL("Enter the address or part of the address");
        var msg = await SendOrUpdate();
        botState.MessageId = msg.MessageId;
         
        var address = await AwaitText(() => Send("Text input timeout. Use /start to try again"));
        await DeletePreviousMessageIfNeeded(botState, chatId.Value);

        var user = await _unitOfWork.TelegramUserInfos.GetByTelegramChatId(chatId.Value);
         
        _logger.LogInformation($"User {user?.TelegramUserFullName} entered text {address} to search for an address");
        
        var telegramToAddressList = new List<TelegramToAddressModel>();

        var labels = (await _geoAdminSearcher.GetAddress(address)).ToList();
        
        foreach (var label in labels)
        {
            var telegramToAddressModel = new TelegramToAddressModel
            {
                FeatureId = label.featureId,
                OriginalAddressLatitude = label.lat.ToString(CultureInfo.InvariantCulture),
                OriginalAddressLongitude = label.lon.ToString(CultureInfo.InvariantCulture),
                Address = label.label
            };
            telegramToAddressList.Add(telegramToAddressModel);

            RowButton(label.label,
                Q(PressAddressToRentButton, telegramToAddressModel,
                    new BotState() { UtilizationTypeId = botState.UtilizationTypeId, SpaceAddress = label.label, StartRent = botState.StartRent, EndRent = botState.EndRent}));
        }

        if (!labels.Any())
        {
            RowButton("Try again"!);
            await SendOrUpdate();
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
        if (botState.UtilizationTypeId == 15 || botState.UtilizationTypeId == 16)
        {
            //only for demo 
            var targetSpacesInside = await _spaceGetter.GetByType(15);
            var targetSpacesOutside = await _spaceGetter.GetByType(16);
            var targetSpaces = targetSpacesInside.Take(1).ToList();
            targetSpaces.AddRange(targetSpacesOutside);
            
            var nearestParkingSpaces = await _spaceService.GetNearestSpaces(targetSpaces, 
                Convert.ToDouble(telegramToAddressModel.OriginalAddressLatitude, CultureInfo.InvariantCulture), Convert.ToDouble(telegramToAddressModel.OriginalAddressLongitude, CultureInfo.InvariantCulture), 7);

            if (nearestParkingSpaces.Any())
            {
                var counter = 1;
                foreach (var nearestParkingSpace in nearestParkingSpaces)
                {
                    var tamModel = new TelegramToAddressModel
                    {
                        latitude = nearestParkingSpace.Value.Latitude.ToString(CultureInfo.InvariantCulture),
                        longitude = nearestParkingSpace.Value.Longitude.ToString(CultureInfo.InvariantCulture),
                        OriginalAddressLatitude = telegramToAddressModel.OriginalAddressLatitude,
                        OriginalAddressLongitude = telegramToAddressModel.OriginalAddressLongitude,
                    };
                    //
                    if (nearestParkingSpace.Value.UtilizationTypeId == 15)
                    {
                        RowButton($"Underground. In {nearestParkingSpace.Key} meters. Tap to details",
                            Q(PressToParkingButton, tamModel,
                                new BotState
                                {
                                    UtilizationTypeId = nearestParkingSpace.Value.UtilizationTypeId, SpaceId = nearestParkingSpace.Value.Id, SpaceAddress =botState.SpaceAddress,
                                    StartRent = botState.StartRent, EndRent = botState.EndRent
                                }));
                        counter++;
                    }
                    else
                    {
                        RowButton($"Street. In {nearestParkingSpace.Key} meters. Tap to details",
                            Q(PressToParkingButton, tamModel,
                                new BotState
                                {
                                    UtilizationTypeId = nearestParkingSpace.Value.UtilizationTypeId, SpaceId = nearestParkingSpace.Value.Id, SpaceAddress =botState.SpaceAddress,
                                    StartRent = botState.StartRent, EndRent = botState.EndRent
                                }));
                        counter++;  
                    }
                }  
                RowButton("↩️ go back", Q(EnterAddress, botState));
         
                PushL($"Found!");
                await SendOrUpdate(); 
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
        var latitude = telegramToAddressModel.latitude.ToString(CultureInfo.InvariantCulture);
        var longitude = telegramToAddressModel.longitude.ToString(CultureInfo.InvariantCulture);
        
        var urlToAddressProperties = $"https://www.google.com/maps/search/?api=1&query={latitude},{longitude}";
        
        
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        var curNumbersStr = (await _unitOfWork.Users.GetByTelegramChatId(chatId.Value)).CarNumbers;
        RowButton("look on the map", urlToAddressProperties);
       
        RowButton("make a reservation", Q(PressMakeAReservation, botState, telegramToAddressModel));
       
        RowButton("️↩️ go back", Q(PressAddressToRentButton, telegramToAddressModel, botState));

        PushL("Details");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressToStartParkingButton(BotState botState)
    {
        RowButton("⏱️ when to park?", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null!));
        RowButton("📍 where to park?", Q(EnterAddress, botState));
        RowButton("↩️ go back", Q<MainController>(m => m.Start));

        PushL("Alright, let’s sort out your parking 🅿️. Just tell me:");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressToEnterTheStartEndTime(BotState botState, RentTimeState rentTimeState, DateTime? dateTime)
    {
     var chatId = Context.GetSafeChatId();
     var messageId = Context.GetMessageId();
        if (!chatId.HasValue) return;
        

        var time = string.Empty;
        if (dateTime != null)
        {
            PushL("Enter the rental start time (in HH:mm format, for example, 14:15)");
            var msg = await SendOrUpdate();
            botState.MessageId = msg.MessageId;
            time = await AwaitText(() => Send("Text input timeout. Use /start to try again"));

            await DeletePreviousMessageIfNeeded(botState, chatId.Value);
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
        DateTime? startTimeToDemo = botState.StartRent.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(botState.StartRent.Value, _zurichTz)
            : null;

        DateTime? endTimeToDemo = botState.EndRent.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(botState.EndRent.Value, _zurichTz)
            : null;
        // var startTimeToDemo = botState.StartRent.HasValue ? botState.StartRent.Value + TimeSpan.FromHours(2) : DateTime.UtcNow;
        // var endTimeToDemo = botState.EndRent.HasValue ? botState.EndRent.Value + TimeSpan.FromHours(2) : DateTime.UtcNow;
        RowButton(botState.StartRent != null ? $"{startTimeToDemo:dddd, MMMM dd yyyy HH:mm}"
                : "⏱️ start time", Q(PressToChooseTheDate, botState, RentTimeState.StartRent));
        RowButton(botState.EndRent != null ? $"{endTimeToDemo:dddd, MMMM dd yyyy HH:mm}"
                : "end time⏱️", Q(PressToChooseTheDate, botState, RentTimeState.EndRent));
            //
        if (botState.StartRent != null && botState.EndRent != null)
        {
            var targetPriceSpecification = new PriceSpecification { PricePerTime = 2.ToString(), PriceCurrency = "CHF"};
              // (await _unitOfWork.PriceSpecifications.GetBySpaceId(botState.SpaceId)).FirstOrDefault(x => x.UnitOfTime == UnitOfDateTime.Hour.ToString());
        
            if (targetPriceSpecification == null) throw new ArgumentNullException("No price specification");
        
            var totalPrice = _priceCalculator.CalculateTotalPriceToReservation(botState.StartRent.Value, botState.EndRent.Value,
                UnitOfDateTime.Hour, targetPriceSpecification.PricePerTime);
            
            //RowButton($"{totalPrice} {targetPriceSpecification.PriceCurrency}. Tap to continue", Q(SpecifyCarNumber, botState));   
            RowButton("📍 where to park?", Q(EnterAddress, botState));
        }
        RowButton("↩️ go back", Q(PressToStartParkingButton, botState));

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

        RowButton("↩️ go back", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null!));
        PushL("choose the day you’ll start parking");
        await SendOrUpdate();
    }

    // [Action]
    // public async Task SpecifyCarNumber(BotState botState, TelegramToAddressModel telegramToAddressModel)
    // {
    //     var chatId = Context.GetSafeChatId();
    //     if (!chatId.HasValue) return;
    //
    //     var curNumbersStr = (await _unitOfWork.Users.GetByTelegramChatId(chatId.Value)).CarNumbers;
    //     if (string.IsNullOrEmpty(curNumbersStr))
    //     {
    //         PushL("Enter your car number");
    //         var msg = await SendOrUpdate();
    //         botState.MessageId = msg.MessageId;
    //         var carNumber = await AwaitText(() => Send("Text input timeout. Use /start to try again"));
    //         botState.CarNumber = carNumber;
    //         await DeletePreviousMessageIfNeeded(botState, chatId.Value);
    //         RowButton("$Reserve a space for {carNumber}",
    //             Q(PressMakeAReservation, botState, telegramToAddressModel));
    //         await SendOrUpdate(); 
    //     }
    //     else
    //     {
    //         var carNumbers = curNumbersStr.Split(";").ToList();
    //         
    //         foreach (var carNumber in carNumbers)
    //         {
    //             RowButton($"{carNumber}",
    //                 Q(PressMakeAReservation,
    //                     new BotState
    //                     {
    //                         UtilizationTypeId = botState.UtilizationTypeId, CarNumber = carNumber,
    //                         SpaceAddress = botState.SpaceAddress, StartRent = botState.StartRent,
    //                         EndRent = botState.EndRent, MessageId = botState.MessageId, SpaceId = botState.SpaceId,
    //                         SpaceName = botState.SpaceName, UtilizationName = botState.UtilizationName
    //                     }, telegramToAddressModel));  
    //         }
    //     }
    //     RowButton("↩️ go back", Q(PressToParkingButton, telegramToAddressModel, botState));
    //     PushL("Specify car number");
    //     await SendOrUpdate(); 
    // }
    
    [Action]
    public async Task PressMakeAReservation(BotState botState, TelegramToAddressModel telegramToAddressModel)
    {
    if (botState.StartRent == null) throw new ArgumentNullException(nameof(botState.StartRent));
        if (botState.EndRent == null) throw new ArgumentNullException(nameof(botState.EndRent));
        
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var curNumbersStr = (await _unitOfWork.Users.GetByTelegramChatId(chatId.Value)).CarNumbers;
        if (string.IsNullOrEmpty(curNumbersStr) && string.IsNullOrEmpty(botState.CarNumber))
        {
            PushL("Enter your car number");
            var msg = await SendOrUpdate();
            botState.MessageId = msg.MessageId;
            var carNumber = await AwaitText(() => Send("Text input timeout. Use /start to try again"));
            botState.CarNumber = carNumber;
            //await DeletePreviousMessageIfNeeded(botState, chatId.Value);
            RowButton($"reserve for {carNumber}",
                Q(PressMakeAReservation, botState, telegramToAddressModel));
            RowButton("↩️ go back", Q(PressToParkingButton, telegramToAddressModel, botState));
            await Send($"Your car number {carNumber}"); 
            await DeletePreviousMessageIfNeeded(botState, chatId.Value);
        }
        else if (!string.IsNullOrEmpty(curNumbersStr))
        {
            var carNumbers = curNumbersStr.Split(";").ToList();
            botState.CarNumber = carNumbers[0];
        }

        try
        {
            var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
            var userWithCurNumber = user;
            userWithCurNumber.CarNumbers = botState.CarNumber;
            await _unitOfWork.Users.Upsert(userWithCurNumber);
            await _unitOfWork.CompleteAsync();
            var targetPriceSpecification = (await _unitOfWork.PriceSpecifications.GetBySpaceId(botState.SpaceId)).FirstOrDefault();
            if (targetPriceSpecification == null) return;
            
            var spacesByType =  await _spaceGetter.GetByType(botState.UtilizationTypeId);
            
            var reservedSpaces = await _reservationService.GetReserved(spacesByType.Select(x=>x.Id), null!, botState.StartRent.Value,
                botState.EndRent.Value);
            
            var availableSpaces = spacesByType.Select(x=>x.Id).Except(reservedSpaces.Select(x=>x.SpaceId)).ToList();
            if (!availableSpaces.Any())
            {
               _logger.LogInformation($"User {user.TelegramUserInfo?.TelegramUserFullName} tried to reserve a space {botState.SpaceName} with id " +
                                      $"{botState.SpaceId}. But this time is already reserved");

               PushL("This time is already reserved");
               RowButton("Go to reservation and try again", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null!));
               await SendOrUpdate();
            }
            else
            {
                var targetSpace = await _spaceGetter.GetById(availableSpaces.First());
                var parkingSpaceNumber = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty)?.Name;
                
                var addReservationModel = new AddReservationModel
                {
                    TenantUserId = user.Id,
                    SpaceId = targetSpace.Id,
                    ReservationDateTimeUtc = DateTime.UtcNow,
                    ReservationFromUtc = botState.StartRent.Value,
                    ReservationToUtc = botState.EndRent.Value,
                    Description = botState.SpaceName
                };
                var reservation = await _reservationService.AddReservation(addReservationModel);

                if (reservation != null)
                {
                    var spaceAddress = (await _unitOfWork.Spaces.GetById(addReservationModel.SpaceId))?.SpaceAddress;
                    
                    var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();
                    RowButton("Go to my reservations", Q(mainControllerService.PressToFindReservationsButton));

                    _logger.LogInformation($"User {user.TelegramUserInfo?.TelegramUserFullName} reserved boat {botState.SpaceName} from " +
                                           $"{botState.StartRent.Value:dddd, MMMM dd yyyy HH:mm} to {botState.EndRent.Value:dddd, MMMM dd yyyy HH:mm} by telegram bot. " +
                                           $"ReservationId: {reservation.Id}");

                    PushL("🎉 Congratulations! Your space reservation has been successfully confirmed. " +
                          "You're all set to enjoy your reserved space. Please find the details below: \n\r \n\r" +
                          $"📅 Date: {botState.StartRent.Value:dddd, MMMM dd}\n\r" +
                          $"⌚ Time: {botState.StartRent.Value:HH:mm}\n\r" + " - " + $"{botState.EndRent.Value:HH:mm}" +
                          $"📍 Location: {spaceAddress}\n\r" +
                          $"📍 Parking Number: {parkingSpaceNumber}\n\r" +
                          $"📍 Car Number: {botState.CarNumber}\n\r" +
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
    
    protected async Task DeletePreviousMessageIfNeeded(BotState state, long chatId)
    {
        if (state.MessageId == default) return;

        await Client.DeleteMessageAsync(chatId, (int)state.MessageId);
        state.MessageId = default;
    }
}