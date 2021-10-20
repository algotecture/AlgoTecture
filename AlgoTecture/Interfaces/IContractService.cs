using System.Threading.Tasks;
using AlgoTecture.Models.Dto;
using AlgoTecture.Models.RepositoryModels;

namespace AlgoTecture.Interfaces
{
    public interface IContractService
    {
        Task<Contract> DeclareContract(ContractDeclarationModel contractDeclarationModel);
        
        Task<Contract> Contract(CompleteContractModel completeContractModel);
    }
}