using System;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ContractController : Controller
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        }
        
        [Authorize]
        [HttpPost("ContractDeclaration")]
        public async Task<ActionResult<Contract>> ContractDeclaration([FromBody] ContractDeclarationModel contractDeclarationModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            return await _contractService.DeclareContract(contractDeclarationModel);
        }

        [Authorize]
        [HttpPost("Contract")]
        public async Task<ActionResult<Contract>> Contract([FromBody]CompleteContractModel completeContractModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            return await _contractService.Contract(completeContractModel);
        }
    }
}