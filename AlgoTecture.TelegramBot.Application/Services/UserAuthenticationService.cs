using AlgoTecture.HttpClient;
using AlgoTecture.Identity.Contracts.Commands;

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

    public UserAuthenticationService(ITelegramBotService telegramBotService, HttpService httpService)
    {
        _telegramBotService = telegramBotService;
        _httpService = httpService;
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
        
        var loginCommand = new TelegramLoginCommand(telegramUserId, fullName);


         var response = await _httpService.PostAsync<TelegramLoginCommand, TelegramLoginResult>(
             "http://localhost:5000/identity/api/auth/telegram-login",
             loginCommand);

        if (response == null || response.IdentityId == Guid.Empty)
            throw new InvalidOperationException("Не удалось зарегистрировать пользователя");

        return response.UserId ?? Guid.Empty;
    }
}