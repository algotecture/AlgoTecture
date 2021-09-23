using System.Threading.Tasks;
using AlgoTec.Models.RepositoryModels;

namespace AlgoTec.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}