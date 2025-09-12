using AlgoTecture.Space.Contracts.Dto;
using MediatR;

namespace AlgoTecture.Space.Contracts.Queries;

public record GetSpacesByTypeQuery(int SpaceTypeId) : IRequest<List<GetSpacesByTypeDto>>;



