namespace AlgoTecMvc.Models.Dto
{
    public class AddSpaceModel
    {
        public string UserEmail { get; set; }

        public string Coordinates { get; set; }

        public int TypeOfSpaceId { get; set; }

        public string Duration { get; set; }
    }
}