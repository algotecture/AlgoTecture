using AlgoTecture.User.Application.Commands;
using AlgoTecture.User.Application.Queries;
using AlgoTecture.User.Contracts.Dto;
using AlgoTecture.User.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{userId:guid}/cars")]
    public async Task<ActionResult<UserCarsDto>> GetCarNumbers(Guid userId, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetUserCarNumbersQuery(userId), ct);
        return Ok(dto);
    }

    [HttpPost("{userId:guid}/cars")]
    public async Task<ActionResult<UserCarsDto>> AddCarNumber(
        Guid userId,
        [FromBody] AddCarNumberRequest request,
        CancellationToken ct)
    {
        var dto = await _mediator.Send(new AddUserCarNumberCommand(userId, request.CarNumber), ct);
        return Ok(dto);
    }
}