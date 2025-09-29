using AlgoTecture.HttpClient;
using AlgoTecture.TelegramBot.Application.Services;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class MainController : BotController
{
    private readonly HttpService _httpService;
    private readonly ITelegramBotService _telegramBotService;
    private readonly IUserAuthenticationService _authService;

    public MainController(HttpService httpService, ITelegramBotService telegramBotService, IUserAuthenticationService authService)
    {
        _httpService = httpService;
        _telegramBotService = telegramBotService;
        _authService = authService;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var userFullName = Context.GetUserFullName();
        if (userId == null) return;

        var linkedUserId = await _authService.EnsureUserAuthenticatedAsync(
            userId.Value,
            chatId!.Value,
            userFullName,
            userName
        );
        if (linkedUserId == Guid.Empty) return;
        //Idustriestrasse 24 8305
        Thread.Sleep(100000);
        PushL("I am your parking 🅿️ assistant. I help you find and manage spots near you.");
        RowButton("🚗 Reserve a parking", Q<ParkingController>(c => c.StartParkingFlow));
        //RowButton("🔍 Reserve a parking", Q(_parkingController.PressToEnterTheStartEndTime()));
        //RowButton("📅 Manage reservations", Q(PressToFindReservationsButton));
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