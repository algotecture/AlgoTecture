using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Enum;
using AlgoTecture.Domain.Models;
using AlgoTecture.Libraries.Environments;
using AlgoTecture.Libraries.PriceSpecifications;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Reservations.Models;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace AlgoTecture.TelegramBot.Controllers;

public class BoatController : BotController, IBoatController
{
    private readonly ISpaceGetter _spaceGetter;
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationService _reservationService;
    private readonly ILogger<BoatController> _logger;

    public BoatController(ISpaceGetter spaceGetter, IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IReservationService reservationService, 
        ILogger<BoatController> logger)
    {
        _spaceGetter = spaceGetter ?? throw new ArgumentNullException(nameof(spaceGetter));
        _serviceProvider = serviceProvider;
        _unitOfWork = unitOfWork;
        _reservationService = reservationService;
        _logger = logger;
    }

    [Action]
    public async Task PressToMainBookingPage(BotState botState)
    {
        //only for demo
        const int boatTargetOfSpaceId = 12;
        if (botState.UtilizationTypeId != boatTargetOfSpaceId)
        {
            return;
        }
        
        RowButton("See available boats", Q(PressToRentTargetUtilizationButton, botState, true));
        RowButton("Make a reservation", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null));

        var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();

        RowButton("Go Back", Q(mainControllerService.PressToRentButton));

        PushL("Reservation");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToRentTargetUtilizationButton(BotState botState, bool isLookingForOnly)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        if (botState.MessageId != default)
        {
            await Client.DeleteMessageAsync(chatId, botState.MessageId);
            botState.MessageId = default;
            botState.SpaceId = default;
            botState.SpaceName = default;
        }
        
        var targetSpaces = await _spaceGetter.GetByType(botState.UtilizationTypeId);

        foreach (var space in targetSpaces)
        {
            var spaceToTelegramOut = new SpaceToTelegramOut
            {
                Name = JsonConvert.DeserializeObject<SpaceProperty>(space.SpaceProperty)?.Name,
                SpaceId = space.Id
            };

            Button(spaceToTelegramOut.Name, Q(PressToSelectTheBoatButton, botState, space.Id, isLookingForOnly));
        }

        RowButton("Go Back", Q(PressToMainBookingPage, botState));

        PushL("Choose Your Desired Boat");
        await SendOrUpdate();
    }

    [Action]
    private async Task PressToChooseTheDate(BotState botState, RentTimeState rentTimeState)
    {
        await Calendar("", botState, false, rentTimeState);
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
        
        RowButton(botState.StartRent != null ? $"{botState.StartRent.Value:dddd, MMMM dd yyyy HH:mm} utc"
                : "Rental start time", Q(PressToChooseTheDate, botState, RentTimeState.StartRent));
        RowButton(botState.EndRent != null ? $"{botState.EndRent.Value:dddd, MMMM dd yyyy HH:mm} utc"
                : "Rental end time", Q(PressToChooseTheDate, botState, RentTimeState.EndRent));
        RowButton(!string.IsNullOrEmpty(botState.SpaceName) ? botState.SpaceName : "Choose a boat", Q(PressToRentTargetUtilizationButton, botState, false));

        if (botState.StartRent != null && botState.EndRent != null && !string.IsNullOrEmpty(botState.SpaceName) && botState.SpaceId != default)
        {
            var targetPriceSpecification = (await _unitOfWork.PriceSpecifications.GetBySpaceId(botState.SpaceId)).FirstOrDefault();
            Enum.TryParse(targetPriceSpecification?.UnitOfTime, out UnitOfDateTime unitOfDateTime);
            var totalPrice = PriceCalculator.CalculateTotalPriceToReservation(botState.StartRent.Value, botState.EndRent.Value,
                unitOfDateTime, targetPriceSpecification.PricePerTime);
            
            RowButton($"Make a reservation! {totalPrice} {targetPriceSpecification.PriceCurrency}", Q(PressMakeAReservation, botState));   
        }

        RowButton("Go Back", Q(PressToMainBookingPage, botState));

        if (string.IsNullOrEmpty(time))
        {
            PushL("Reservation the boat");
            await SendOrUpdate();   
        }
        else
        {
            await Send("Reservation the boat"); 
        }
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

        RowButton("Go Back", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null));
        PushL("Pick the date");
        await SendOrUpdate();
    }
    
    [Action]
    private async Task PressToSelectTheBoatButton(BotState botState, long spaceId, bool isLookingForOnly)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        botState.SpaceId = spaceId;
        var targetSpace = await _spaceGetter.GetById(botState.SpaceId);

        var targetPriceSpecification = (await _unitOfWork.PriceSpecifications.GetBySpaceId(spaceId)).FirstOrDefault();

        string price = string.Empty;
        
        if (targetPriceSpecification != null)
        {
            price = $"{targetPriceSpecification.PricePerTime} {targetPriceSpecification.PriceCurrency.ToUpper()} per {targetPriceSpecification.UnitOfTime.ToLower()}";
        }
        
        var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

        if (targetSpaceProperty == null) throw new ArgumentNullException(nameof(targetSpaceProperty));

        //find the file without extension
        var pathToBoatImage =
            System.IO.Path.Combine(AlgoTectureEnvironments.GetPathToImages(), "Boats", $"{targetSpaceProperty.SpacePropertyId}.jpeg");

        await using var stream = File.OpenRead(pathToBoatImage);
        var inputOnlineFile = new InputOnlineFile(stream, targetSpaceProperty.Name);

        var message = await Client.SendPhotoAsync(
            chatId: chatId,
            photo: inputOnlineFile,
            caption: $"<b>Price: {price}</b>" + "\n" +
                     $"<b></b>",
            ParseMode.Html
        );

        botState.MessageId = message.MessageId;
        
        PushL($"{targetSpaceProperty.Name}");

        if (!isLookingForOnly)
        {
            botState.SpaceName = targetSpaceProperty.Name;
            RowButton("Make a reservation!", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null));  
        }
        
        RowButton("Go Back", Q(PressToRentTargetUtilizationButton, botState, isLookingForOnly));
        
        await SendOrUpdate();
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
            var targetPriceSpecification = (await _unitOfWork.PriceSpecifications.GetBySpaceId(botState.SpaceId)).FirstOrDefault();
            if (targetPriceSpecification == null) return;
            
            Enum.TryParse(targetPriceSpecification?.UnitOfTime, out UnitOfDateTime unitOfDateTime);
            var totalPrice = PriceCalculator.CalculateTotalPriceToReservation(botState.StartRent.Value, botState.EndRent.Value,
                unitOfDateTime, targetPriceSpecification.PricePerTime);
            
            var addOrUpdateReservationModel = new AddOrUpdateReservationModel()
            {
                TenantUserId = user.Id,
                SpaceId = botState.SpaceId,
                PriceSpecificationId = targetPriceSpecification.Id,
                ReservationDateTimeUtc = DateTime.UtcNow,
                ReservationFromUtc = botState.StartRent,
                ReservationToUtc = botState.EndRent,
                ReservationStatus = ReservationStatusType.Confirmed.ToString(),
                TotalPrice = totalPrice,
                Description = botState.SpaceName
            };
            var checkedReservation = await _reservationService.CheckReservation(botState.SpaceId, null, botState.StartRent.Value,
                botState.EndRent.Value);
            if (checkedReservation != null)
            {
               _logger.LogInformation($"Telegram User {user.TelegramUserInfo?.TelegramUserFullName} tried to reserved space {botState.SpaceName} with id " +
                                      $"{botState.SpaceId}. But this time is already reserved");

               PushL("This time is already reserved");
               RowButton("Go to reservation and try again", Q(PressToEnterTheStartEndTime, botState, RentTimeState.None, null));
               await SendOrUpdate();
            }
            else
            {
                var reservation = await _reservationService.AddReservation(addOrUpdateReservationModel);

                if (reservation != null)
                {
                    var mainControllerService = _serviceProvider.GetRequiredService<IMainController>();
                    RowButton("Go to my reservations", Q(mainControllerService.PressToFindReservationsButton));
                
                    _logger.LogInformation($"User {user.TelegramUserInfo?.TelegramUserFullName} reserved boat {botState.SpaceName} from " +
                                           $"{botState.StartRent.Value:dddd, MMMM dd yyyy HH:mm} to {botState.EndRent.Value:dddd, MMMM dd yyyy HH:mm} by telegram bot. " +
                                           $"ReservationId: {reservation.Id}");
                
                    PushL("Object successfully reserved");
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
}