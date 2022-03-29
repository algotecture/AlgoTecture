using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.EfCli;
using AlgoTecture.Persistence.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Persistence.Core.Repositories;

public class TelegramUserInfoRepository : GenericRepository<TelegramUserInfo>, ITelegramUserInfoRepository
{
    public TelegramUserInfoRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<TelegramUserInfo> Upsert(TelegramUserInfo entity)
    {
        try
        {
            var existingTelegramUserInfo = await dbSet.Where(x => x.Id == entity.Id)
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