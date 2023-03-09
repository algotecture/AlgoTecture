using AlgoTecture.Libraries.Reservation.Models;

namespace AlgoTecture.Libraries.Reservation;

public interface IReservationService
{
    Task<Domain.Models.RepositoryModels.Reservation> AddOrUpdateReservation(AddOrUpdateReservationModel addOrUpdateReservationModel);
}