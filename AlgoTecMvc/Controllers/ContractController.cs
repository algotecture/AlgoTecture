using System;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Models;
using AlgoTecMvc.Models.Dto;
using AlgoTecMvc.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoTecMvc.Controllers
{
    public class ContractController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ActionResult<Contract>> AddContract(AddContractModel addContractModel)
        {
            
            var targetUser = await _unitOfWork.Users.GetByEmail(addContractModel.UserEmail);
            var targetUserId = default(long);
            if (targetUser == null)
            {
                var newUser = new User
                {
                    Email =  addContractModel.UserEmail
                };
                targetUser = await _unitOfWork.Users.Add(newUser);
                await _unitOfWork.CompleteAsync();
            }
            targetUserId = targetUser.Id;
            var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(addContractModel.SelectedSpaceProperty);
            var targetSpaceId = targetSpaceProperty.SpaceId;
            var targetSpacePropertyId = targetSpaceProperty.SpacePropertyId;
            var targetOwnerId = targetSpaceProperty.OwnerId;
            var isValidDurationInDays = int.TryParse(addContractModel.Duration, out var targetDurationInDays);
            var newContract = new Contract
            {
                OwnerUserId = targetOwnerId,
                TenantUserId = targetUserId,
                SpaceId = targetSpaceId,
                SpacePropertyId = targetSpacePropertyId,
                ContractDateStart = DateTime.UtcNow,
                ContractDateStop = DateTime.UtcNow.AddDays(targetDurationInDays)
            };
            
            var createdContract = await _unitOfWork.Contracts.Add(newContract);
            await _unitOfWork.CompleteAsync();

            return createdContract;
        }
        
    }
}