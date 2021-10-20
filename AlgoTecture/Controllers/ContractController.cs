using System;
using System.Threading.Tasks;
using AlgoTecture.Interfaces;
using AlgoTecture.Models.Dto;
using AlgoTecture.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.Controllers
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