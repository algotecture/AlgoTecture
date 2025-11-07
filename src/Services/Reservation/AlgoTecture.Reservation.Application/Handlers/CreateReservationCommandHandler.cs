using AlgoTecture.Reservation.Application.Commands;
using AlgoTecture.Reservation.Contracts.Dto;
using AlgoTecture.Reservation.Domain;
using AlgoTecture.Reservation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Reservation.Application.Handlers;

public class CreateReservationCommandHandler
    : IRequestHandler<CreateReservationCommand, ReservationDto>
{
    private readonly ReservationDbContext _db;

    public CreateReservationCommandHandler(ReservationDbContext db) => _db = db;

    public async Task<ReservationDto> Handle(CreateReservationCommand cmd, CancellationToken ct)
    {
        var req = cmd.Request;
//ToDo Pending only for demo
        var overlap = await _db.Reservations.AnyAsync(x =>
            x.SpaceId == req.SpaceId &&
            x.Status == ReservationStatus.Pending &&
            x.StartUtc < req.EndUtc &&
            req.StartUtc < x.EndUtc, ct);
//ToDo correct way to check overlap
        if (overlap)
        {
            return new ReservationDto(
                Guid.Empty, Guid.Empty, Guid.Empty, DateTimeOffset.MinValue, DateTimeOffset.MinValue, ReservationStatus.Cancelled,
                0, string.Empty, string.Empty, string.Empty, DateTimeOffset.MinValue);
        }

        var reservation = new Domain.Reservation(
            req.SpaceId,
            req.UserId,
            req.StartUtc,
            req.EndUtc,
            req.CarNumber);

        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync(ct);

        return new ReservationDto(
            reservation.Id,
            reservation.SpaceId,
            reservation.UserId,
            reservation.StartUtc,
            reservation.EndUtc,
            reservation.Status,
            reservation.TotalPrice,
            reservation.Currency,
            reservation.PublicId,
            req.CarNumber,
            reservation.CreatedAtUtc);
    }
}