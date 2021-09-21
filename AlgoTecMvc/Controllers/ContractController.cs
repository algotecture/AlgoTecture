using System;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Interfaces;
using AlgoTecMvc.Models.Dto;
using AlgoTecMvc.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecMvc.Controllers
{
    [Route("[controller]")]
    public class ContractController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContractService _contractService;

        public ContractController(IUnitOfWork unitOfWork, IContractService contractService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        }

        public async Task<ActionResult<Contract>> ContractDeclaration([FromBody] ContractDeclarationModel contractDeclarationModel)
        {
            return await _contractService.DeclareContract(contractDeclarationModel);
        }

        [HttpPost("CompleteContract")]
        public async Task<ActionResult<Contract>> CompleteContract([FromBody]CompleteContractModel completeContractModel)
        {
            var targetUser = await _unitOfWork.Users.GetByEmail(completeContractModel.UserEmail);
            var targetUserId = default(long);
            if (targetUser == null)
            {
                var newUser = new User
                {
                    Email =  completeContractModel.UserEmail
                };
                targetUser = await _unitOfWork.Users.Add(newUser);
                await _unitOfWork.CompleteAsync();
            }

            var targetContract = await _unitOfWork.Contracts.GetByGuid(completeContractModel.ContractId);
            targetContract.TenantUserId = targetUser.Id;

            var updatedContract = await _unitOfWork.Contracts.Upsert(targetContract);
            
            await _unitOfWork.CompleteAsync();

            if (completeContractModel.RestApi)
            {
                return updatedContract;
            }
            
            TempData["Success"] = "Contract updated successfully!";
            return RedirectToAction("Index", "Home");
        }
        
    }
}