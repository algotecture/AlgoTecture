﻿using System.ComponentModel.DataAnnotations;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Libraries.Contract.Interfaces;
using AlgoTecture.Data.Persistence.Core.Interfaces;

namespace AlgoTecture.Libraries.Contract.Implementations
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Domain.Models.RepositoryModels.Contract> DeclareContract(ContractDeclarationModel contractDeclarationModel)
        {
            if (contractDeclarationModel == null) throw new ArgumentNullException(nameof(contractDeclarationModel));

            var targetUser = await _unitOfWork.Users.GetByEmail(contractDeclarationModel.UserEmail);

            if (targetUser == null) throw new ArgumentNullException(nameof(targetUser));

            var isExistContract = await _unitOfWork.Contracts.IsActiveContract(contractDeclarationModel.SubSpaceId, contractDeclarationModel.DateStop);

            if (isExistContract) throw new ValidationException("This space has a contract");

            var newContractDeclaration = new Domain.Models.RepositoryModels.Contract
            {
                OwnerUserId = targetUser.Id,
                TenantUserId = null,
                SpaceId = contractDeclarationModel.SpaceId,
                SubSpaceId = contractDeclarationModel.SubSpaceId,
                ContractFrom = contractDeclarationModel.DateStart,
                ContractTo = contractDeclarationModel.DateStop,
                TotalPrice = contractDeclarationModel.TotalPrice,
                UtilizationTypeId = contractDeclarationModel.UtilizationTypeId,
                DeclarationDateTime = DateTime.UtcNow,
                ContractDateTime = null
            };

            var createdContractDeclaration = await _unitOfWork.Contracts.Add(newContractDeclaration);
            await _unitOfWork.CompleteAsync();

            return createdContractDeclaration;
        }

        public async Task<Domain.Models.RepositoryModels.Contract> Contract(CompleteContractModel completeContractModel)
        {
            if (completeContractModel == null) throw new ArgumentNullException(nameof(completeContractModel));

            var targetUser = await _unitOfWork.Users.GetByEmail(completeContractModel.UserEmail);

            var targetContract = await _unitOfWork.Contracts.GetByGuid(completeContractModel.ContractId);
            targetContract.TenantUserId = targetUser.Id;
            targetContract.ContractDateTime = DateTime.UtcNow;

            var updatedContract = await _unitOfWork.Contracts.Upsert(targetContract);

            await _unitOfWork.CompleteAsync();

            return updatedContract;
        }
    }
}