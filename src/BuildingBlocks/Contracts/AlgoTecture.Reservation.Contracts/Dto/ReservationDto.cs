using AlgoTecture.Reservation.Domain;

namespace AlgoTecture.Reservation.Contracts.Dto;

public record ReservationDto(
    Guid Id,
    Guid SpaceId,
    Guid UserId,
    DateTimeOffset StartUtc,
    DateTimeOffset EndUtc,
    ReservationStatus Status,
    decimal? TotalPrice,
    string? Currency,
    string? PublicId,
    string? CarNumber,
    DateTimeOffset CreatedAtUtc);