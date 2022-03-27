using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Interfaces
{
    public interface IContractService
    {
        Task<Contract> DeclareContract(ContractDeclarationModel contractDeclarationModel);
        
        Task<Contract> Contract(CompleteContractModel completeContractModel);
    }
}