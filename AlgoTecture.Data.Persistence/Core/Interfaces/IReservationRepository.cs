using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Data.Persistence.Core.Interfaces;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    Task<IEnumerable<Reservation>> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo);
    
    Task<IEnumerable<Reservation>> GetReservationsByUserId(long userId);
    
    Task<IEnumerable<Reservation>> GetReservationsBySpaceId(long spaceId);
    
    Task<Reservation?> GetByReservationUniqueIdentifier(string reservationUniqueIdentifier);
}