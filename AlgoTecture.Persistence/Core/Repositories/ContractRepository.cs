using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.EfCli;
using AlgoTecture.Persistence.Core.Interfaces;
using AlgoTecture.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlgoTecture.Persistence.Core.Repositories
{
    public class ContractRepository : GenericRepository<Contract>, IContractRepository
    {
        public ContractRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }
        
        public override async Task<IEnumerable<Contract>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ContractRepository));
                return new List<Contract>();
            }
        }
        
        public override async Task<Contract> GetByGuid(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<bool> IsActiveContract(Guid spacePropertyId, DateTime dateStart)
        {
            return await dbSet.AnyAsync(x => x.SubSpaceId == spacePropertyId && x.ContractDateStart <= dateStart && x.ContractDateStop >= dateStart);
        }

        public override async Task<Contract> Upsert(Contract entity)
        {
            try
            {
                var existingContract = await dbSet.Where(x => x.Id == entity.Id)
                    .FirstOrDefaultAsync();

                if (existingContract == null)
                    return await Add(entity);

                existingContract.TenantUserId = entity.TenantUserId;
                existingContract.ContractDateTime = entity.ContractDateTime;
                
                return existingContract;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(ContractRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }     
    }
}