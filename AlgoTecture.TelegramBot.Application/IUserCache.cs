namespace AlgoTecture.TelegramBot.Application;

public interface IUserCache
{
    Task<Guid?> GetUserIdByTelegramAsync(long telegramUserId, CancellationToken ct = default);
    Task SetUserIdByTelegramAsync(long telegramUserId, Guid userId, TimeSpan? ttl = null, CancellationToken ct = default);
}
