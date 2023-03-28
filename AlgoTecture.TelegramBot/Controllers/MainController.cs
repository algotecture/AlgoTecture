using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Libraries.UtilizationTypes;
using AlgoTecture.TelegramBot.Controllers.Interfaces;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Controllers;

public class MainController : BotController, IMainController
{
    private readonly ITelegramUserInfoService _telegramUserInfoService;
    private readonly IUtilizationTypeGetter _utilizationTypeGetter;
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IBoatController _boatController;

    public MainController(ITelegramUserInfoService telegramUserInfoService, IBoatController boatController, IUtilizationTypeGetter utilizationTypeGetter, IUnitOfWork unitOfWork)
    {
        _telegramUserInfoService = telegramUserInfoService;
        _boatController = boatController;
        _utilizationTypeGetter = utilizationTypeGetter;
        _unitOfWork = unitOfWork;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var fullUserName = Context.GetUserFullName();
        var addTelegramUserInfoModel = new AddOrUpdateTelegramUserInfoModel
        {
            TelegramUserId = userId,
            TelegramChatId = chatId,
            TelegramUserName = userName,    
            TelegramUserFullName = fullUserName
        };

        _ = await _telegramUserInfoService.AddOrUpdate(addTelegramUserInfoModel);

        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");

        Button("I want to rent", Q(PressToRentButton));
        Button("I have a reservation", Q(PressToFindReservationsButton));
        RowButton("Manage the contract", Q(PressToManageContract));
    }
    
    [Action]
    public async Task PressToRentButton()
    {
        var utilizationTypes = (await _utilizationTypeGetter.GetAll()).Skip(6).ToList();

        var utilizationTypeToTelegramList = new List<UtilizationTypeToTelegramOut>();
        foreach (var utilizationType in utilizationTypes)
        {
            var utilizationTypeOut = new UtilizationTypeToTelegramOut
            {
                Name = utilizationType.Name,
                Id = utilizationType.Id
            };

            utilizationTypeToTelegramList.Add(utilizationTypeOut);

            var botState = new BotState { UtilizationTypeId = utilizationType.Id, MessageId = default(int) };

            RowButton(utilizationType.Name, Q(_boatController.PressToMainBookingPage, botState));   
        }
        RowButton("Go Back", Q(Start));

        PushL("Choose your boat");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressToFindReservationsButton()
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var user = await _unitOfWork.Users.GetByTelegramChatId(chatId.Value);
        var reservations = await _unitOfWork.Reservations.GetReservationsByUserId(user.Id);

        var reservationList = new List<ReservationToTelegramOut>();
        
        foreach (var reservation in reservations)
        {
            var reservationToTelegram = new ReservationToTelegramOut()
            {
                Id = reservation.Id,
                DateTimeFrom = $"{reservation.ReservationFromUtc.Value:dd MM yyyy HH:mm} utc",
                DateTimeTo = $"{reservation.ReservationToUtc.Value:dd MM yyyy HH:mm} utc",
                Description = reservation.Description,
                TotlaPrice = reservation.TotalPrice,
                PriceCurrency = reservation.PriceSpecification.PriceCurrency
            };
            reservationList.Add(reservationToTelegram);
            var description =
                $"{reservationToTelegram.Description}, {reservationToTelegram.DateTimeFrom} - {reservationToTelegram.DateTimeTo}, {reservationToTelegram.TotlaPrice} {reservationToTelegram.PriceCurrency.ToUpper()}";
            RowButton(description, Q(PressToManageContract));
        }
        RowButton("Go Back", Q(Start));
        
        PushL("Reservations");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressToManageContract()
    {
      
    }
}