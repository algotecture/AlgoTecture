

using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Persistence.Core.Interfaces
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        Task<bool> IsActiveContract(Guid spacePropertyId, DateTime dateStart);
    }
}