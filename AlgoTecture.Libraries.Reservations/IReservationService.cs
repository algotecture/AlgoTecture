using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations.Models;
using JetBrains.Annotations;

namespace AlgoTecture.Libraries.Reservations;

public interface IReservationService
{
    Task<Reservation?>? AddOrUpdateReservation(AddOrUpdateReservationModel addOrUpdateReservationModel);
}