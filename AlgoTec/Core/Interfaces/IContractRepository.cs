using System;
using System.Threading.Tasks;
using AlgoTec.Models.RepositoryModels;

namespace AlgoTec.Core.Interfaces
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        Task<bool> IsActiveContract(Guid spacePropertyId, DateTime dateStart);
    }
}