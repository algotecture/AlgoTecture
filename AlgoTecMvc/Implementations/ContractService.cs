using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Interfaces;
using AlgoTecMvc.Models.Dto;
using AlgoTecMvc.Models.RepositoryModels;

namespace AlgoTecMvc.Implementations
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Contract> DeclareContract(ContractDeclarationModel contractDeclarationModel)
        {
            var targetUser = await _unitOfWork.Users.GetByEmail(contractDeclarationModel.UserEmail);

            if (targetUser == null) throw new ArgumentNullException(nameof(targetUser));

            var isExistContract = await _unitOfWork.Contracts.IsActiveContract(contractDeclarationModel.SpacePropertyId, contractDeclarationModel.DateStop);

            if (isExistContract) throw new ValidationException("This space has a contract");

            var newContractDeclaration = new Contract
            {
                OwnerUserId = targetUser.Id,
                TenantUserId = null,
                SpaceId = contractDeclarationModel.SpaceId,
                SpacePropertyId = contractDeclarationModel.SpacePropertyId,
                ContractDateStart = contractDeclarationModel.DateStart,
                ContractDateStop = contractDeclarationModel.DateStop,
                Cost = contractDeclarationModel.Cost
            };

            var createdContractDeclaration = await _unitOfWork.Contracts.Add(newContractDeclaration);
            await _unitOfWork.CompleteAsync();

            return createdContractDeclaration;
        }
    }
}