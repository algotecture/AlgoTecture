using System.Threading.Tasks;
using AlgoTecMvc.Models.Dto;
using AlgoTecMvc.Models.RepositoryModels;

namespace AlgoTecMvc.Interfaces
{
    public interface IContractService
    {
        Task<Contract> DeclareContract(ContractDeclarationModel contractDeclarationModel);
        
        Task<Contract> Contract(CompleteContractModel completeContractModel);
    }
}