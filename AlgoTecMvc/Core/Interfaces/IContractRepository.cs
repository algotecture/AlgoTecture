using System;
using System.Threading.Tasks;
using AlgoTecMvc.Models.RepositoryModels;

namespace AlgoTecMvc.Core.Interfaces
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        Task<bool> IsActiveContract(Guid spacePropertyId, DateTime dateStart);
    }
}