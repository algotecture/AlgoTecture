using System;
using System.Threading.Tasks;
using AlgoTec.Interfaces;
using AlgoTec.Models.Dto;
using AlgoTec.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTec.Controllers
{
    [Route("[controller]")]
    public class ContractController : Controller
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        }
        
        [HttpPost("ContractDeclaration")]
        public async Task<ActionResult<Contract>> ContractDeclaration([FromBody] ContractDeclarationModel contractDeclarationModel)
        {
            return await _contractService.DeclareContract(contractDeclarationModel);
        }

        [HttpPost("Contract")]
        public async Task<ActionResult<Contract>> Contract([FromBody]CompleteContractModel completeContractModel)
        {
            return await _contractService.Contract(completeContractModel);
        }
    }
}