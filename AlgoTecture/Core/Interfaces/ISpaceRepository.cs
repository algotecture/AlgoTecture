using System.Threading.Tasks;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Core.Interfaces
{
    public interface ISpaceRepository : IGenericRepository<Space>
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}