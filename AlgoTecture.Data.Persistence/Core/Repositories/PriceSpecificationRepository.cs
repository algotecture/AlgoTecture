using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Ef;
using AlgoTecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Data.Persistence.Core.Repositories;

public class PriceSpecificationRepository : GenericRepository<PriceSpecification>, IPriceSpecificationRepository
{
    public PriceSpecificationRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<PriceSpecification>> GetBySpaceId(long spaceId)
    {
        return await dbSet.Where(x => x.SpaceId == spaceId).ToListAsync();
    }
}