using AlgoTecture.Space.Contracts.Dto;
using AlgoTecture.Space.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.Space.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpaceController : ControllerBase
{
    private readonly IMediator _mediator;
    public SpaceController(IMediator mediator) => _mediator = mediator;

    [HttpGet("by-type/{spaceTypeId}")]
    public async Task<ActionResult<List<SpaceDto>>> GetBySpaceType(int spaceTypeId)
        => Ok(await _mediator.Send(new GetSpacesByTypeQuery(spaceTypeId)));
    
    [HttpGet("nearest-by-type/{latitude}/{longitude}/{spaceTypeId}/{maxDistanceMeters}/{count}")]
    public async Task<ActionResult<List<SpaceDto>>> GetNearestBySpaceType(double latitude, double longitude, int spaceTypeId, int maxDistanceMeters, int count)
        => Ok(await _mediator.Send(new GetNearestSpacesByTypeQuery(latitude, longitude, spaceTypeId, maxDistanceMeters, count)));
}
