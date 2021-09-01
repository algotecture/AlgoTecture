using System;

namespace AlgoTecMvc.Models.Dto
{
    public class AddContractModel
    {
        public string UserEmail { get; set; }
        public string SelectedSpaceProperty { get; set; }

        public long SpaceId { get; set; }

        public Guid SpacePropertyId { get; set; }
        
        public string DateStart { get; set; }

        public string DateStop { get; set; }

        public bool RestApi { get; set; }
    }
}