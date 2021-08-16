namespace AlgoTecMvc.Models.Dto
{
    public class AddContractModel
    {
        public string UserEmail { get; set; }

        public string SelectedSpaceProperty { get; set; }

        public long SpaceId { get; set; }

        public string Duration { get; set; }   
    }
}