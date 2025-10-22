using AlgoTecture.HttpClient;
using AlgoTecture.Identity.Contracts;
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
    private readonly IAuthApi _authApi;

    public UserAuthenticationService(ITelegramBotService telegramBotService, IAuthApi authApi)
    {
        _telegramBotService = telegramBotService;
        _authApi = authApi;
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

        var response = await _authApi.TelegramLoginAsync(loginCommand);

        if (response == null || response.IdentityId == Guid.Empty)
            throw new InvalidOperationException("Не удалось зарегистрировать пользователя");

        return response.UserId ?? Guid.Empty;
    }
}