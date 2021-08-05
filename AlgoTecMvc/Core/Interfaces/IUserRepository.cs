using System.Threading.Tasks;
using AlgoTecMvc.Models.RepositoryModels;

namespace AlgoTecMvc.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}