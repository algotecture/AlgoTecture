using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Data.Persistence.Ef;
using Algotecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Algotecture.Data.Persistence.Core.Repositories;

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
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        if (entity.TelegramChatId == null) throw new ArgumentNullException(nameof(entity.TelegramChatId));

        try
        {
            var existingTelegramUserInfo = await dbSet.FirstOrDefaultAsync(x => x.TelegramChatId == entity.TelegramChatId.Value);
            
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