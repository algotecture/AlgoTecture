using System.Threading.Tasks;
using AlgoTec.Models.Dto;
using AlgoTec.Models.RepositoryModels;

namespace AlgoTec.Interfaces
{
    public interface IContractService
    {
        Task<Contract> DeclareContract(ContractDeclarationModel contractDeclarationModel);
        
        Task<Contract> Contract(CompleteContractModel completeContractModel);
    }
}