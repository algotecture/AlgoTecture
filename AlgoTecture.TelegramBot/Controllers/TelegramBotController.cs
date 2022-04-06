using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.GeoAdminSearch.Models.GeoAdminModels;
using AlgoTecture.TelegramBot.Interfaces;
using AlgoTecture.TelegramBot.Models;
using Deployf.Botf;
using Volo.Abp.Modularity;

namespace AlgoTecture.TelegramBot.Controllers;

[DependsOn(typeof(GeoAdminSearcher))]
public class TelegramBotController : BotController
{
    private readonly GeoAdminSearcher _geoAdminSearcher;
    readonly BotfOptions _options;
    private readonly ITelegramUserInfoService _telegramUserInfoService;
    private readonly ITelegramToAddressResolver _telegramToAddressResolver;
    
    public TelegramBotController(GeoAdminSearcher geoAdminSearcher, ITelegramUserInfoService telegramUserInfoService, ITelegramToAddressResolver telegramToAddressResolver)
    {
        _geoAdminSearcher = geoAdminSearcher ?? throw new ArgumentNullException(nameof(geoAdminSearcher));
        _telegramUserInfoService = telegramUserInfoService ?? throw new ArgumentNullException(nameof(telegramUserInfoService));
        _telegramToAddressResolver = telegramToAddressResolver ?? throw new ArgumentNullException(nameof(telegramToAddressResolver));
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
        Button("I want to rent", Q(PressToRentButton));
        Button("I want to find", Q(PressTryToFindButton));
    }
    
    [Action]
    private async Task PressToRentButton()
    {
        PushL("Enter the address or part of the address");
        await Send(); 
        var term =  await AwaitText();
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var telegramToAddressList = new List<TelegramToAddressModel>();
        
        
        var labels = await _geoAdminSearcher.GetAddress(term);
        foreach (var label in labels)
        {
            var telegramToAddressModel = new TelegramToAddressModel
            {
                FeatureId = label.featureId,
                latitude = label.lat,
                longitude = label.lon,
                Address = label.label
            };
            telegramToAddressList.Add(telegramToAddressModel);
            RowButton(label.label, Q(PressAddressToRentButton, label.featureId));
        }
        _telegramToAddressResolver.TryAddCurrentAddressList(chatId.Value, telegramToAddressList);
        await Send("Choose the right address");
    }
    
    [Action]
    private async Task PressAddressToRentButton(string geoAdminFeatureId)
    {
        var chatId = Context.GetSafeChatId();
        if (!chatId.HasValue) return;

        var targetAddress = _telegramToAddressResolver.TryGetAddressListByChatId(chatId.Value).FirstOrDefault(x=>x.FeatureId == geoAdminFeatureId);
        _telegramToAddressResolver.RemoveAddressListByChatId(chatId.Value);
        
        if (targetAddress == null) return;
        
        await Send(targetAddress.Address);
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