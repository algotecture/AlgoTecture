using Algotecture.Space.Contracts.Dto;
using MediatR;

namespace Algotecture.Space.Contracts.Queries;

public record GetSpacesByTypeQuery(int SpaceTypeId) : IRequest<List<GetSpacesByTypeDto>>;



