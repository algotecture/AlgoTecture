using System;

namespace AlgoTecMvc.Models.Dto
{
    public class ContractDeclarationModel
    {
        public string UserEmail { get; set; }
        //public string SelectedSpaceProperty { get; set; }

        public long SpaceId { get; set; }

        public Guid SpacePropertyId { get; set; }
        
        public DateTime DateStart { get; set; }

        public DateTime DateStop { get; set; }

        public decimal Cost { get; set; }

        public bool RestApi { get; set; }
    }
}