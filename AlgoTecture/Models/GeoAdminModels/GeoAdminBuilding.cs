namespace AlgoTecture.Models.GeoAdminModels
{
    public class GeoAdminBuilding
    {
        public int BuildingYear { get; set; }

        public string BuildingCategory { get; set; }

        public string BuildingClass { get; set; }

        public int Levels { get; set; }

        public int Area { get; set; }

        public int FloorArea { get; set; }

        public int Flats { get; set; }
        
        public string PlaceName { get; set; }

        public int MunicipalityId { get; set; }

        public string MunicipalityName { get; set; }
    }
}