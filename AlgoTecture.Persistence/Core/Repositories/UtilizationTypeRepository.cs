using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.EfCli;
using AlgoTecture.Persistence.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Persistence.Core.Repositories;

public class UtilizationTypeRepository : GenericRepository<UtilizationType>, IUtilizationTypeRepository
{
    public UtilizationTypeRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

    
    public override async Task<IEnumerable<UtilizationType>> All()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} All function error", typeof(UtilizationTypeRepository));
            return new List<UtilizationType>();
        }
    }

    public override async Task<UtilizationType> Upsert(UtilizationType entity)
    {
        try
        {
            var existingUtilizationType = await dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (existingUtilizationType == null)
                return await Add(entity);

            return existingUtilizationType;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Upsert function error", typeof(UtilizationTypeRepository));
            throw;
        }
    }

    public override async Task<bool> Delete(long id)
    {
        try
        {
            var exist = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null) return false;

            dbSet.Remove(exist);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Delete function error", typeof(UtilizationTypeRepository));
            return false;
        }
    }
}