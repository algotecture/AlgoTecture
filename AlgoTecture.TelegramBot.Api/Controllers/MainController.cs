using AlgoTecture.HttpClient;
using AlgoTecture.TelegramBot.Api.Interfaces;
using AlgoTecture.TelegramBot.Application.Services;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class MainController : BotController, IMainController
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
        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");

        //RowButton("🔍 Explore & 📌 Reserve Spaces", Q(PressToRentButton));
        //RowButton("📅 Control & 📝 Manage Reservations", Q(PressToFindReservationsButton));
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