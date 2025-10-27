using AlgoTecture.Space.Contracts.Dto;
using Refit;

namespace AlgoTecture.Space.Contracts;

public interface ISpaceApi
{
    [Get("/api/space/nearest/{latitude}/{longitude}/{spaceTypeId}/{maxDistanceMeters}/{count}")]
    Task<List<SpaceDto>> GetNearestSpacesAsync(double latitude, double longitude, int spaceTypeId, int maxDistanceMeters, int count);
}