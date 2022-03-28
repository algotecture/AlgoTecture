using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Persistence.Core.Interfaces
{
    public interface IUserAuthenticationRepository : IGenericRepository<UserAuthentication>
    {
        Task<bool> IsValidPasswordAsync(long userId, string hashedPassword);
    }
}