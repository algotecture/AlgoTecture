using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.Reservations.Models;

namespace Algotecture.Libraries.Reservations;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo);

    Task<Reservation?> AddReservation(AddReservationModel addReservationModel);

    Task<Reservation?> UpdateReservation(UpdateReservationModel updateReservationModel);

    Task<IEnumerable<Reservation>> GetReservationsBySpaceId(long spaceId);

    Task<Reservation?> UpdateReservationStatus(string reservationStatus, long reservationId);
    
    Task<Reservation?> GetByReservationUniqueIdentifier(string reservationUniqueIdentifier);
}