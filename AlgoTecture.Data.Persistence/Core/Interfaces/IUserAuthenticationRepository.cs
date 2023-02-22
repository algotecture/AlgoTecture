using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Data.Persistence.Core.Interfaces
{
    public interface IUserAuthenticationRepository : IGenericRepository<UserAuthentication>
    {
        Task<bool> IsValidPasswordAsync(long userId, string hashedPassword);
    }
}