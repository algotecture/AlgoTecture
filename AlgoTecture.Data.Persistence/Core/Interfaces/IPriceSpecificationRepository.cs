using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Data.Persistence.Core.Interfaces;

public interface IPriceSpecificationRepository : IGenericRepository<PriceSpecification>
{
    Task<IEnumerable<PriceSpecification>> GetBySpaceId(long spaceId);   
}