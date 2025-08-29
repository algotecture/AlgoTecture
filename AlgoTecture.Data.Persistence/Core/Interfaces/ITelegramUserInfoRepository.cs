using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Data.Persistence.Core.Interfaces;

public interface ITelegramUserInfoRepository : IGenericRepository<TelegramUserInfo>
{
    Task<TelegramUserInfo?> GetByTelegramChatId(long chatId);
}