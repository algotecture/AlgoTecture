using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Data;
using AlgoTecMvc.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecMvc.Core.Repositories
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

        public override async Task<Space> Upsert(Space entity)
        {
            try
            {
                var existingSpace = await dbSet.Where(x => x.Id == entity.Id)
                    .FirstOrDefaultAsync();

                if (existingSpace == null)
                    return await Add(entity);

                existingSpace.SpaceProperty= entity.SpaceProperty;

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