using System.Threading.Tasks;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Core.Interfaces
{
    public interface IUserAuthenticationRepository : IGenericRepository<UserAuthentication>
    {
        Task<bool> IsValidPasswordAsync(long userId, string hashedPassword);
    }
}