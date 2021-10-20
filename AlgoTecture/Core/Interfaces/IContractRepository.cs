using System;
using System.Threading.Tasks;
using AlgoTecture.Models.RepositoryModels;

namespace AlgoTecture.Core.Interfaces
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        Task<bool> IsActiveContract(Guid spacePropertyId, DateTime dateStart);
    }
}