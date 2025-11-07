using AlgoTecture.Reservation.Contracts.Dto;
using AlgoTecture.Reservation.Contracts.Requests;
using MediatR;

namespace AlgoTecture.Reservation.Application.Commands;

public record CreateReservationCommand(CreateReservationRequest Request) : IRequest<ReservationDto>;
