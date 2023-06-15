using System.Threading.Tasks;
using AlgoTecture.Domain.Models.RepositoryModels;
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
    
    [HttpPost("AddReservation")]
    public async Task<ActionResult<Reservation>> AddReservation([FromBody] AddReservationModel addReservationModel)
    {
        if (!ModelState.IsValid) return BadRequest();

        var reservation = await _reservationService.AddReservation(addReservationModel);

        return reservation;
    } 

    [HttpPost("UpdateReservationStatus")]
    public async Task<ActionResult<IResult>> UpdateReservationStatus([FromBody] UpdateReservationStatusModel updateReservationStatusModel)
    {
        if (!ModelState.IsValid) return BadRequest();

        _ = await _reservationService.UpdateReservationStatus(updateReservationStatusModel.ReservationStatus, updateReservationStatusModel.ReservationId);

        return Ok();
    } 
    
    [HttpGet("GetByReservationUniqueIdentifier")]
    public async Task<ActionResult<Reservation>> GetByReservationUniqueIdentifier([FromQuery] string reservationUniqueIdentifier)
    {
        if (!ModelState.IsValid) return BadRequest();

        return await _reservationService.GetByReservationUniqueIdentifier(reservationUniqueIdentifier);
    } 
}