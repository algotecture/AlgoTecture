using System.Threading.Tasks;
using AlgoTecMvc.Models.RepositoryModels;

namespace AlgoTecMvc.Core.Interfaces
{
    public interface ISpaceRepository : IGenericRepository<Space>
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}