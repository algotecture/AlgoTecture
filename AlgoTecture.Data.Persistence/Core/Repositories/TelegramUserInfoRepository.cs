using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Ef;
using AlgoTecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Data.Persistence.Core.Repositories;

public class TelegramUserInfoRepository : GenericRepository<TelegramUserInfo>, ITelegramUserInfoRepository
{
    public TelegramUserInfoRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public async Task<TelegramUserInfo?> GetByTelegramChatId(long chatId)
    {
        
        return await dbSet.FirstOrDefaultAsync(x => x.TelegramChatId == chatId);
    }

    public override async Task<TelegramUserInfo> Upsert(TelegramUserInfo entity)
    {
        try
        {
            var existingTelegramUserInfo = await dbSet.Where(x => x.TelegramChatId == entity.TelegramChatId)
                .FirstOrDefaultAsync();

            if (existingTelegramUserInfo == null)
                return await Add(entity);

            existingTelegramUserInfo.TelegramUserName = entity.TelegramUserName;
            existingTelegramUserInfo.TelegramUserFullName = entity.TelegramUserFullName;

            return existingTelegramUserInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
            throw;
        }
    }
}