using AlgoTecture.Reservation.Contracts.Dto;
using MediatR;

namespace AlgoTecture.Reservation.Application.Queries;

public record GetUserReservationsQuery(Guid UserId, bool OnlyActive) : IRequest<IReadOnlyList<ReservationDto>>;
