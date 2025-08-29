using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Data.Persistence.Core.Interfaces;

public interface IPriceSpecificationRepository : IGenericRepository<PriceSpecification>
{
    Task<IEnumerable<PriceSpecification>> GetBySpaceId(long spaceId);   
}