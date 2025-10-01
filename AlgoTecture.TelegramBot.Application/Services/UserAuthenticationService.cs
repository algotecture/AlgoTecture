using AlgoTecture.HttpClient;
using AlgoTecture.Identity.Contracts.Commands;
using Identity.Grpc;

namespace AlgoTecture.TelegramBot.Application.Services;

public interface IUserAuthenticationService
{
    Task<Guid> EnsureUserAuthenticatedAsync(
        long telegramUserId,
        long chatId,
        string fullName,
        string? username);
}

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly ITelegramBotService _telegramBotService;
    private readonly HttpService _httpService;
    private readonly TelegramAuth.TelegramAuthClient _client;

    public UserAuthenticationService(ITelegramBotService telegramBotService, HttpService httpService, TelegramAuth.TelegramAuthClient client)
    {
        _telegramBotService = telegramBotService;
        _httpService = httpService;
        _client = client;
    }

    public async Task<Guid> EnsureUserAuthenticatedAsync(
        long telegramUserId,
        long chatId,
        string fullName,
        string? username)
    {
        var linkedUserId = await _telegramBotService.GetUserIdByTelegram(
            telegramUserId, chatId, fullName, username, string.Empty);

        if (linkedUserId != Guid.Empty)
        {
            return linkedUserId;
        }
        
        //var loginCommand = new TelegramLoginCommand(telegramUserId, fullName);

        var response = await _client.TelegramLoginAsync(new TelegramLoginRequest
        {
            TelegramUserId = telegramUserId,
            TelegramUserFullName = fullName
        });
        // var response = await _httpService.PostAsync<TelegramLoginCommand, TelegramLoginResult>(
        //     "http://localhost:5000/identity/api/auth/telegram-login",
        //     loginCommand);

        if (string.IsNullOrEmpty(response.UserId))
            throw new InvalidOperationException("Не удалось зарегистрировать пользователя");

        return Guid.Parse(response.UserId);
    }
}