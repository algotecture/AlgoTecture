using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Data.Persistence.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmail(string email);

        Task<User> GetByTelegramChatId(long telegramChatId);
    }
}