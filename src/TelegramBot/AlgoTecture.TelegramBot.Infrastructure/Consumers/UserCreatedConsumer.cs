using AlgoTecture.TelegramBot.Application;
using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using AlgoTecture.User.Contracts.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.TelegramBot.Infrastructure.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly TelegramAccountDbContext _db;
    private readonly IUserCache _cache;

    public UserCreatedConsumer(TelegramAccountDbContext db, IUserCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var message = context.Message;

        var telegramAccount = await _db.TelegramAccounts
            .FirstOrDefaultAsync(x => x.TelegramUserId.ToString() == message.ProviderUserId);

        if (telegramAccount != null)
        {
            telegramAccount.LinkedUserId = message.UserId;
            await _db.SaveChangesAsync();
            await _cache.SetUserIdByTelegramAsync(telegramAccount.TelegramUserId!.Value, telegramAccount.LinkedUserId.Value, TimeSpan.FromDays(30), default);
        }
    }
}