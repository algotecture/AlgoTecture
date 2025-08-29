using Algotecture.Domain.Models.Dto;
using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Libraries.Spaces.Interfaces
{
    public interface ISpaceGetter
    {
        Task<Space?> GetByCoordinates(double latitude, double longitude);
        
        Task<List<Space>> GetByType(int utilizationTypeId);

        Task<Space?> GetById(long spaceId);

        Task<SpaceWithProperty?> GetByIdWithProperty(long spaceId);
    }
}