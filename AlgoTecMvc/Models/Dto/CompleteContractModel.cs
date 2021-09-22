using System;

namespace AlgoTecMvc.Models.Dto
{
    public class CompleteContractModel
    {
        public string UserEmail { get; set; }
        
        public Guid ContractId { get; set; }
    }
}