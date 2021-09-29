using System;

namespace AlgoTec.Models.Dto
{
    public class ContractDeclarationModel
    {
        public string UserEmail { get; set; }

        public long SpaceId { get; set; }

        public Guid SpacePropertyId { get; set; }
        
        public DateTime DateStart { get; set; }

        public DateTime DateStop { get; set; }

        public decimal Cost { get; set; }

        public int UtilizationTypeId { get; set; }
        
    }
}