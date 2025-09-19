using AlgoTecture.HttpClient;
using AlgoTecture.Identity.Contracts.Commands;
using AlgoTecture.TelegramBot.Api.Interfaces;
using Deployf.Botf;

namespace AlgoTecture.TelegramBot.Api.Controllers;

public class MainController : BotController, IMainController
{
    private readonly HttpService _httpService;

    public MainController(HttpService httpService)
    {
        _httpService = httpService;
    }

    [Action("/start", "start the bot")]
    public async Task Start()
    {
        var chatId = Context.GetSafeChatId();
        var userId = Context.GetSafeUserId();
        var userLogin = Context.GetUsername();
        var userFullName = Context.GetUserFullName();
        if (userId == null) return;
        
        var telegramLoginCommand = new TelegramLoginCommand(userId.Value, userFullName);
        
        var response = await _httpService.PostAsync<TelegramLoginCommand, TelegramLoginResult>(
            "\"/identity/api/auth/telegram-login\"",
            telegramLoginCommand
        );


        //_logger.LogInformation($"User {user.TelegramUserFullName} logged in by telegram bot");

        PushL("I am your assistant 💁‍♀️ in searching and renting sustainable spaces around the globe 🌍 (test mode)");

        RowButton("🔍 Explore & 📌 Reserve Spaces", Q(PressToRentButton));
        RowButton("📅 Control & 📝 Manage Reservations", Q(PressToFindReservationsButton));
    }
}