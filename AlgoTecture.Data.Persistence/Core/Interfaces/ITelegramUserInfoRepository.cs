using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Data.Persistence.Core.Interfaces;

public interface ITelegramUserInfoRepository : IGenericRepository<TelegramUserInfo>
{
    Task<TelegramUserInfo?> GetByTelegramChatId(long chatId);
}