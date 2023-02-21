using AlgoTecture.Domain.Models.Dto;

namespace AlgoTecture.Libraries.Contract.Interfaces
{
    public interface IContractService
    {
        Task<Domain.Models.RepositoryModels.Contract> DeclareContract(ContractDeclarationModel contractDeclarationModel);
        
        Task<Domain.Models.RepositoryModels.Contract> Contract(CompleteContractModel completeContractModel);
    }
}