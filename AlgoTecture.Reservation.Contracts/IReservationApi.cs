using AlgoTecture.Reservation.Contracts.Dto;
using AlgoTecture.Reservation.Contracts.Requests;
using Refit;

namespace AlgoTecture.Reservation.Contracts;

public interface IReservationApi
{
    [Post("/api/reservations")]
    Task<ReservationDto> CreateReservation([Body] CreateReservationRequest request);

    [Get("/api/reservations")]
    Task<IReadOnlyList<ReservationDto>> GetUserReservations(
        [Query] Guid? userId = null,
        [Query] bool onlyActive = true);
}
