using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using AlgoTecture.User.Contracts.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.TelegramBot.Infrastructure.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly TelegramAccountDbContext _db;

    public UserCreatedConsumer(TelegramAccountDbContext db)
    {
        _db = db;
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
        }
    }
}