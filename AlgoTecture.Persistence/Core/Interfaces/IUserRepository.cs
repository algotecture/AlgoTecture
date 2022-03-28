using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Persistence.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}