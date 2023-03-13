using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations.Models;

namespace AlgoTecture.Libraries.Reservations;

public interface IReservationService
{
    Task<Reservation> AddOrUpdateReservation(AddOrUpdateReservationModel addOrUpdateReservationModel);
}