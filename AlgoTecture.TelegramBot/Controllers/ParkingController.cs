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
    public async Task PressToMainBookingPage(BotState botState, RentTimeState rentTimeState, DateTime? dateTime)
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
        var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();
        RowButton("Choose a parking space", Q(PressMakeAReservation, botState));
        RowButton("Go Back", Q(mainControllerService.PressToRentButton));

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
                    Q(PressToMainBookingPage, botState, rentTimeState, date));
        }
        else
        {
            calendar = calendar
                .Year(now.Year).Month(now.Month)
                .Depth(CalendarDepth.Days)
                .SetState(state)
                .OnNavigatePath(s => Q(Calendar, s, botState, true, rentTimeState))
                .OnSelectPath(date =>
                    Q(PressToMainBookingPage, botState, rentTimeState, date))
                .SkipDay(d => d.Day < now.Day);
        }

        calendar.Build(Message, new PagingService());

        RowButton("Go Back", Q(PressToMainBookingPage, botState, RentTimeState.None, null!));
        PushL("Pick the date");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressMakeAReservation(BotState botState)
    {
        //only for demo
        const int parkingTargetOfSpaceId = 15;
        if (botState.UtilizationTypeId != parkingTargetOfSpaceId)
        {
            return;
        }
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        if (botState.MessageId != default)
        {
            await Client.DeleteMessageAsync(chatId, botState.MessageId);
            botState.MessageId = default;
            botState.SpaceId = default;
            botState.SpaceName = default;
        }
        // var chatId = Context.GetSafeChatId();
        // if (!chatId.HasValue) return;
        // var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
        // var checkedReservation = await _reservationService.CheckReservation(botState.SpaceId, null!, botState.StartRent.Value,
        //     botState.EndRent.Value);
        // if (checkedReservation.Any())
        // {
        //     _logger.LogInformation($"User {user.TelegramUserInfo?.TelegramUserFullName} tried to reserve a space {botState.SpaceName} with id " +
        //                            $"{botState.SpaceId}. But this time is already reserved");
        //
        //     PushL("This time is already reserved");
        //     RowButton("Go to previous and try again", Q(PressToMainBookingPage, botState, RentTimeState.None, null!));
        //     await SendOrUpdate();
        // }
        
        RowButton("Street parking", Q(PressFloorButton, new BotState{UtilizationTypeId = 11, SpaceName = "Street parking", StartRent = botState.StartRent, EndRent = botState.EndRent}));
        RowButton("P -1", Q(PressFloorButton, new BotState{UtilizationTypeId = 11, SpaceName = "P -1", StartRent = botState.StartRent, EndRent = botState.EndRent}));
        RowButton("P -2", Q(PressFloorButton, new BotState{UtilizationTypeId = 11, SpaceName = "P -2", StartRent = botState.StartRent, EndRent = botState.EndRent}));
        RowButton("P -3", Q(PressFloorButton, new BotState{UtilizationTypeId = 11, SpaceName = "P -3", StartRent = botState.StartRent, EndRent = botState.EndRent}));
        RowButton("P -4", Q(PressFloorButton, new BotState{UtilizationTypeId = 11, SpaceName = "P -4", StartRent = botState.StartRent, EndRent = botState.EndRent}));
        RowButton("Floor and location don't matter. Any",
            Q(PressFloorButton,
                new BotState { UtilizationTypeId = 11, SpaceName = "Floor and location don't matter. Any" , StartRent = botState.StartRent, EndRent = botState.EndRent}));
        
        RowButton("Go Back", Q(PressToMainBookingPage, botState, RentTimeState.None, null!));
        
        PushL("Select parking floor");
        await SendOrUpdate();   
    }

    [Action]
    public async Task PressFloorButton(BotState botState)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;
        
        if (botState.SpaceName == "Floor and location don't matter. Any")
        {
            return;
        }

        var targetSpace = await _spaceGetter.GetById(3379);
        if (targetSpace != null)
        {
            var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

            if (targetSpaceProperty == null) throw new ArgumentNullException(nameof(targetSpaceProperty));
            
            var targetPriceSpecification = (await _unitOfWork.PriceSpecifications.GetBySpaceId(3379)).FirstOrDefault();
            if (targetPriceSpecification == null) return;

            var imageNames = targetSpaceProperty.Images;

            if (imageNames != null && imageNames.Any())
            {
                var pathToBoatImage =
                    System.IO.Path.Combine(AlgoTectureEnvironments.GetPathToImages(), "Spaces",
                        targetSpace.Id.ToString(), imageNames.First());

                await using var stream = File.OpenRead(pathToBoatImage);
                var inputOnlineFile = new InputOnlineFile(stream, targetSpaceProperty.Name);

                var message = await Client.SendPhotoAsync(
                    chatId: chatId,
                    photo: inputOnlineFile,
                    caption: $"<b>Price: {targetPriceSpecification.PricePerTime} per hour</b>" + "\n" +
                             $"<b>Buttons above image 👆</b>",
                    ParseMode.Html
                );

                botState.MessageId = message.MessageId;
                
            }
            var targetSpaces = await _spaceGetter.GetByType(15);
            
            var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
            
            var listToExclude = new List<Space>();
            foreach (var space in targetSpaces)
            {
                var checkedReservation = await _reservationService.CheckReservation(space.Id, null!, botState.StartRent.Value,
                    botState.EndRent.Value);
                if (checkedReservation.Any())
                {
                    listToExclude.Add(space);
                }
            }
            
            var resultSpaces = targetSpaces.Except(listToExclude);
           
           
            
            if (botState.SpaceName == "Street parking")
            {

            }
            if (botState.SpaceName == "P -1")
            {

            }
            if (botState.SpaceName == "P -2")
            {

            }
            if (botState.SpaceName == "P -3")
            {

            }
            if (botState.SpaceName == "P -4")
            {

            }
            RowButton("Go Back", Q(PressMakeAReservation, botState));
            PushL($"Floor plan");
        }
        
        
    await SendOrUpdate();
    }
}