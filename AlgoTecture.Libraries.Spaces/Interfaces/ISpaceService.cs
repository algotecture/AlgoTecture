using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Spaces.Models.Dto;

namespace AlgoTecture.Libraries.Spaces.Interfaces;

public interface ISpaceService
{
    Task<Space> AddSpace(AddSpaceModel addSpaceModel);

    Task<Space> UpdateSpace(UpdateSpaceModel updateSpaceModel);

    Task<List<KeyValuePair<int,Space>>> GetNearestSpaces(IEnumerable<Space> spaces, double latitude, double longitude,
        int spaceCountToOut);
}