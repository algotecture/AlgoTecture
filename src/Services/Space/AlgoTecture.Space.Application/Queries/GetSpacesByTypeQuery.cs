using AlgoTecture.Space.Contracts.Dto;
using MediatR;

namespace AlgoTecture.Space.Application.Queries;

public record GetSpacesByTypeQuery(int SpaceTypeId) : IRequest<List<SpaceDto>>;

public record GetNearestSpacesByTypeQuery(
    double Latitude,
    double Longitude,
    int SpaceTypeId,
    int MaxDistanceMeters = 10000,
    int Count = 10) : IRequest<List<SpaceDto>>;



