using AlgoTecture.HttpClient;
using AlgoTecture.Identity.Contracts.Commands;
using AlgoTecture.TelegramBot.Api.Interfaces;
using AlgoTecture.TelegramBot.Application.Services;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class MainController : BotController, IMainController
{
    private readonly HttpService _httpService;
    private readonly ITelegramBotService _telegramBotService;

    public MainController(HttpService httpService, ITelegramBotService telegramBotService)
    {
        _httpService = httpService;
        _telegramBotService = telegramBotService;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userName = Context.GetUsername();
        var userFullName = Context.GetUserFullName();
        if (userId == null) return;
        
        var linkedUserId = await _telegramBotService.GetUserIdByTelegram(userId.Value, chatId!.Value, userFullName, userName, string.Empty);
        if (linkedUserId == Guid.Empty)
        {
            var telegramLoginCommand = new TelegramLoginCommand(userId.Value, userFullName);
            var response = await _httpService.PostAsync<TelegramLoginCommand, TelegramLoginResult>(
                "http://localhost:5000/identity/api/auth/telegram-login",
                telegramLoginCommand
            );
        }
        
        //_logger.LogInformation($"User {user.TelegramUserFullName} logged in by telegram bot");
        Thread.Sleep(100000);
        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");

       // RowButton("🔍 Explore & 📌 Reserve Spaces", Q(PressToRentButton));
      //  RowButton("📅 Control & 📝 Manage Reservations", Q(PressToFindReservationsButton));
    }
}