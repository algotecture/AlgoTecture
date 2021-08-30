namespace AlgoTecMvc.Models.Dto
{
    public class AddSpaceModel
    {
        public string UserEmail { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int TypeOfSpaceId { get; set; }
    }
}