using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AlgoTec.Core.Interfaces;
using AlgoTec.Interfaces;
using AlgoTec.Models.Dto;
using AlgoTec.Models.RepositoryModels;

namespace AlgoTec.Implementations
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
            if (contractDeclarationModel == null) throw new ArgumentNullException(nameof(contractDeclarationModel));

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

        public async Task<Contract> Contract(CompleteContractModel completeContractModel)
        {
            if (completeContractModel == null) throw new ArgumentNullException(nameof(completeContractModel));

            var targetUser = await _unitOfWork.Users.GetByEmail(completeContractModel.UserEmail);

            var targetContract = await _unitOfWork.Contracts.GetByGuid(completeContractModel.ContractId);
            targetContract.TenantUserId = targetUser.Id;

            var updatedContract = await _unitOfWork.Contracts.Upsert(targetContract);

            await _unitOfWork.CompleteAsync();

            return updatedContract;
        }
    }
}