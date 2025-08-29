using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Data.Persistence.Ef;
using Algotecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Algotecture.Data.Persistence.Core.Repositories
{
    public class SpaceRepository : GenericRepository<Space>, ISpaceRepository
    {
        public SpaceRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Space>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(SpaceRepository));
                return new List<Space>();
            }
        }

        public async Task<Space?> GetByCoordinates(double latitude, double longitude)
        {
            return await dbSet.Include(x=>x.UtilizationType)
                .FirstOrDefaultAsync(x=>Math.Abs(x.Latitude - latitude) < 0.000000001 && Math.Abs(x.Longitude - longitude) < 0.000000001);
        }
        
        public async Task<List<Space>> GetByType(int utilizationTypeId)
        {
            return await dbSet.Where(x=>x.UtilizationTypeId == utilizationTypeId).ToListAsync();
        }

        public override async Task<Space> Upsert(Space entity)
        {
            try
            {
                var existingSpace = await dbSet.Where(x => x.Id == entity.Id)
                    .FirstOrDefaultAsync();

                if (existingSpace == null)
                    return await Add(entity);

                existingSpace.SpaceProperty = entity.SpaceProperty;
                existingSpace.Latitude = entity.Latitude;
                existingSpace.Longitude = entity.Longitude;
                existingSpace.SpaceAddress = entity.SpaceAddress;
                existingSpace.UtilizationTypeId = entity.UtilizationTypeId;

                return existingSpace;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(SpaceRepository));
                return false;
            }
        }   
    }
}