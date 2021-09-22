using System;
using System.Threading.Tasks;
using AlgoTecMvc.Interfaces;
using AlgoTecMvc.Models.Dto;
using AlgoTecMvc.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecMvc.Controllers
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