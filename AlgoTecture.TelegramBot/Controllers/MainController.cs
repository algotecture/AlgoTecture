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
    
    private readonly IBoatController _boatController;

    public MainController(ITelegramUserInfoService telegramUserInfoService, IBoatController boatController, IUtilizationTypeGetter utilizationTypeGetter)
    {
        _telegramUserInfoService = telegramUserInfoService;
        _boatController = boatController;
        _utilizationTypeGetter = utilizationTypeGetter;
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
        Button("I have a booking", Q(PressToFindBooking));
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

            RowButton(utilizationType.Name, Q(_boatController.PressToMainBookingPage, utilizationType.Id, default(int)));   
        }
        RowButton("Go Back", Q(Start));

        PushL("Choose your boat");
        await SendOrUpdate();
    }
    
    [Action]
    public async Task PressToFindBooking()
    {
      
    }
    
    [Action]
    public async Task PressToManageContract()
    {
      
    }
}