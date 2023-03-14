using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Data.Persistence.Core.Interfaces;

public interface IReservationRepository: IGenericRepository<Reservation>
{
    Task<Reservation?> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo);
}