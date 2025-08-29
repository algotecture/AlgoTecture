using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Data.Persistence.Core.Interfaces
{
    public interface IUserAuthenticationRepository : IGenericRepository<UserAuthentication>
    {
        Task<bool> IsValidPasswordAsync(long userId, string hashedPassword);
    }
}