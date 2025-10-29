namespace AlgoTecture.Reservation.Contracts.Requests;

public record CreateReservationRequest(
    Guid SpaceId,
    Guid UserId,
    DateTimeOffset StartUtc,
    DateTimeOffset EndUtc,
    string? CarNumber = null);