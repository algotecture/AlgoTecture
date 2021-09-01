namespace AlgoTecMvc.Models.Dto
{
    public class AddSpaceModel
    {
        public string UserEmail { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int TypeOfSpaceId { get; set; }

        public SubSpace SubSpace { get; set; }

        public string DateStart { get; set; }

        public string DateStop { get; set; }

        public bool RestApi { get; set; }
    }
}