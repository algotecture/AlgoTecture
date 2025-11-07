using AlgoTecture.TelegramBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.TelegramBot.Application.Services;

public interface ITelegramBotService
{
    Task<Guid> GetUserIdByTelegram(long telegramUserId, long telegramChatId, string? userFullName, string? userName,
        string? languageCode, CancellationToken ct = default);
}

public class TelegramBotService : ITelegramBotService
{
    private readonly IUserCache _cache;
    private readonly ITelegramAccountDbContext _db;

    public TelegramBotService(IUserCache cache, ITelegramAccountDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    public async Task<Guid> GetUserIdByTelegram(long telegramUserId, long telegramChatId, string? userFullName, string? userName, string? languageCode, CancellationToken ct = default)
    {
        var cached = await _cache.GetUserIdByTelegramAsync(telegramUserId, ct);
        if (cached != null)
            return cached.Value;
        
        var telegramAccount = await _db.TelegramAccounts.FirstOrDefaultAsync(x=>x.TelegramUserId == telegramUserId, cancellationToken: ct);
        
        if (telegramAccount == null)
        {
            var newTelegramAccount = new TelegramAccount
            {
                Id = Guid.NewGuid(),
                TelegramUserId = telegramUserId,
                TelegramChatId = telegramChatId,
                TelegramUserFullName = userFullName,
                TelegramUserName = userName,
                LanguageCode = languageCode,
                CreatedAt = DateTime.UtcNow,
                LastActivityAt = DateTime.UtcNow
            };
            await _db.TelegramAccounts.AddAsync(newTelegramAccount, ct);
            await _db.SaveChangesAsync(ct);
            
            return Guid.Empty;
        }

        if (telegramAccount.LinkedUserId.HasValue)
        {
            await _cache.SetUserIdByTelegramAsync(telegramUserId, telegramAccount.LinkedUserId.Value, TimeSpan.FromDays(30), ct);
            return telegramAccount.LinkedUserId.Value;
        }
        
        return Guid.Empty;
    }
}