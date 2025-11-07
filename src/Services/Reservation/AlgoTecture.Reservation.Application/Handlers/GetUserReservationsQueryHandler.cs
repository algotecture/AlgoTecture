using AlgoTecture.Reservation.Application.Queries;
using AlgoTecture.Reservation.Contracts.Dto;
using AlgoTecture.Reservation.Domain;
using AlgoTecture.Reservation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Reservation.Application.Handlers;

public class GetUserReservationsQueryHandler
    : IRequestHandler<GetUserReservationsQuery, IReadOnlyList<ReservationDto>>
{
    private readonly ReservationDbContext _db;
    public GetUserReservationsQueryHandler(ReservationDbContext db) => _db = db;

    public async Task<IReadOnlyList<ReservationDto>> Handle(GetUserReservationsQuery q, CancellationToken ct)
    {
        var query = _db.Reservations.AsNoTracking()
            .Where(x => x.UserId == q.UserId);

        if (q.OnlyActive)
            query = query.Where(x => x.Status == ReservationStatus.Pending || x.Status == ReservationStatus.Confirmed);

        return await query
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new ReservationDto(
                x.Id,
                x.SpaceId,
                x.UserId,
                x.StartUtc,
                x.EndUtc,
                x.Status,
                x.TotalPrice,
                x.Currency,
                x.PublicId,
                CarFromMeta(x.Metadata),
                x.CreatedAtUtc))
            .ToListAsync(ct);
    }
    
    private static string? CarFromMeta(string? metadata)
    {
        if (string.IsNullOrWhiteSpace(metadata)) return null;
        try
        {
            using var doc = System.Text.Json.JsonDocument.Parse(metadata);
            return doc.RootElement.TryGetProperty("carNumber", out var v) ? v.GetString() : null;
        }
        catch { return null; }
    }
}