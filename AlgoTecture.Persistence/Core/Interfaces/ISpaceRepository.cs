using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Persistence.Core.Interfaces
{
    public interface ISpaceRepository : IGenericRepository<Space>
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}