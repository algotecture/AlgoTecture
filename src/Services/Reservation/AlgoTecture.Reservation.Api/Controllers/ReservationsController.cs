using AlgoTecture.Reservation.Application.Commands;
using AlgoTecture.Reservation.Application.Queries;
using AlgoTecture.Reservation.Contracts.Dto;
using AlgoTecture.Reservation.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateReservation(
        [FromBody] CreateReservationRequest request,
        CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateReservationCommand(request), ct);
        return CreatedAtAction(nameof(GetByUser), new { userId = result.UserId }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReservationDto>>> GetByUser(
        [FromQuery] Guid userId,
        [FromQuery] bool onlyActive = true,
        CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetUserReservationsQuery(userId, onlyActive), ct);
        return Ok(result);
    }
}