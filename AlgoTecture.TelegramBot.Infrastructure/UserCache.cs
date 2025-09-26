using System.Text.Json;
using AlgoTecture.TelegramBot.Application;
using Microsoft.Extensions.Caching.Distributed;

namespace AlgoTecture.TelegramBot.Infrastructure;

public class UserCache : IUserCache
{
    private readonly IDistributedCache _cache;

    public UserCache(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    private string Key(long telegramUserId) => $"user:by-telegram:{telegramUserId}";

    public async Task<Guid?> GetUserIdByTelegramAsync(long telegramUserId, CancellationToken ct = default)
    {
        var data = await _cache.GetStringAsync(Key(telegramUserId), ct);
        if (string.IsNullOrEmpty(data))
            return null;

        return JsonSerializer.Deserialize<Guid>(data);
    }

    public async Task SetUserIdByTelegramAsync(long telegramUserId, Guid userId, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(userId);
        var options = new DistributedCacheEntryOptions();
        if (ttl != null)
            options.AbsoluteExpirationRelativeToNow = ttl;

        await _cache.SetStringAsync(Key(telegramUserId), json, options, ct);
    }
}