using System.Threading.Tasks;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Reservations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.WebApi.Controllers;

[Route("[controller]")]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost("UpdateReservationStatus")]
    public async Task<ActionResult<IResult>> UpdateReservationStatus([FromBody] UpdateReservationStatusModel updateReservationStatusModel)
    {
        if (!ModelState.IsValid) return BadRequest();

        _ = await _reservationService.UpdateReservationStatus(updateReservationStatusModel.ReservationStatus, updateReservationStatusModel.ReservationId);

        return Ok();
    } 
}