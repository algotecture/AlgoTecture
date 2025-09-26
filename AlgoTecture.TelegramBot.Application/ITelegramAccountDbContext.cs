using AlgoTecture.TelegramBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.TelegramBot.Application;

public interface ITelegramAccountDbContext
{
    DbSet<TelegramAccount> TelegramAccounts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}