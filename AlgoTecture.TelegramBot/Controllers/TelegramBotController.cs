using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.TelegramBot.Interfaces;
using Deployf.Botf;
using Volo.Abp.Modularity;

namespace AlgoTecture.TelegramBot.Controllers;

[DependsOn(typeof(GeoAdminSearcher))]
public class TelegramBotController : BotController
{
    private readonly GeoAdminSearcher _geoAdminSearcher;
    readonly BotfOptions _options;
    private readonly ITelegramUserInfoService _telegramUserInfoService;
    
    public TelegramBotController(GeoAdminSearcher geoAdminSearcher, ITelegramUserInfoService telegramUserInfoService)
    {
        _geoAdminSearcher = geoAdminSearcher ?? throw new ArgumentNullException(nameof(geoAdminSearcher));
        _telegramUserInfoService = telegramUserInfoService ?? throw new ArgumentNullException(nameof(telegramUserInfoService));
    }
    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var fullUserName = Context.GetUserFullName();
        var addTelegramUserInfoModel = new AddTelegramUserInfoModel
        {
            TelegramUserId = userId,
            TelegramChatId = chatId,
            TelegramUserName = userName,
            TelegramUserFullName = fullUserName
        };

        var targetTelegramUserInfo = await _telegramUserInfoService.Create(addTelegramUserInfoModel);
        
        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");
        RowButton("Try to find!", Q(PressTryToFindButton));
    }

    [Action]
    private async Task PressTryToFindButton()
    {
        PushL("Enter the address or part of the address");
        await Send(); 
        var term =  await AwaitText();

        var labels = await _geoAdminSearcher.GetAddress(term);
        foreach (var label in labels)
        {
            RowButton(label.label, Q(PressAddressButton));
        }
        await Send("You won");
    }

    [Action]
    private async Task PressAddressButton()
    {
        PushL("Have a good day");  
        await Send();
    }
}