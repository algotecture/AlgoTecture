using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Data.Persistence.Core.Interfaces
{
    public interface ISpaceRepository : IGenericRepository<Space>
    {
        Task<Space?> GetByCoordinates(double latitude, double longitude);
        
        Task<List<Space>> GetByType(int utilizationTypeId);
    }
}