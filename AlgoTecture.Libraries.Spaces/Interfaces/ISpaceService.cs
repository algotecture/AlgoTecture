using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.Spaces.Models.Dto;

namespace Algotecture.Libraries.Spaces.Interfaces;

public interface ISpaceService
{
    Task<Space> AddSpace(AddSpaceModel addSpaceModel);

    Task<Space> UpdateSpace(UpdateSpaceModel updateSpaceModel);

    Task<List<KeyValuePair<int,Space>>> GetNearestSpaces(IEnumerable<Space> spaces, double latitude, double longitude,
        int spaceCountToOut);
}