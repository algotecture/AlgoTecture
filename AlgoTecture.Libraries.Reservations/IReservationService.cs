using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations.Models;
using JetBrains.Annotations;

namespace AlgoTecture.Libraries.Reservations;

public interface IReservationService
{
    Task<Reservation?> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo);

    Task<Reservation?> AddReservation(AddOrUpdateReservationModel addOrUpdateReservationModel);

    Task<Reservation?> UpdateReservation(AddOrUpdateReservationModel addOrUpdateReservationModel);

    Task<IEnumerable<Reservation>> GetReservationsBySpaceId(long spaceId);
}