using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations.Models;

namespace AlgoTecture.Libraries.Reservations;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo);

    Task<Reservation?> AddReservation(AddReservationModel addReservationModel, string pricePerTime = null);

    Task<Reservation?> UpdateReservation(UpdateReservationModel updateReservationModel);

    Task<IEnumerable<Reservation>> GetReservationsBySpaceId(long spaceId);

    Task<Reservation?> UpdateReservationStatus(string reservationStatus, long reservationId);
    
    Task<Reservation?> GetByReservationUniqueIdentifier(string reservationUniqueIdentifier);
}