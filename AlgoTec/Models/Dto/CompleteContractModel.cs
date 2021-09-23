using System;

namespace AlgoTec.Models.Dto
{
    public class CompleteContractModel
    {
        public string UserEmail { get; set; }
        
        public Guid ContractId { get; set; }
    }
}