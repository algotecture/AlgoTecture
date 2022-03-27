using System.Threading.Tasks;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}