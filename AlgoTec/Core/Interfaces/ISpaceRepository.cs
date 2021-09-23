using System.Threading.Tasks;
using AlgoTec.Models.RepositoryModels;

namespace AlgoTec.Core.Interfaces
{
    public interface ISpaceRepository : IGenericRepository<Space>
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}