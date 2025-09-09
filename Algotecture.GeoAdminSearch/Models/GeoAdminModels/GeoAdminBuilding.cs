namespace Algotecture.GeoAdminSearch.Models.GeoAdminModels
{
    public class GeoAdminBuilding
    {
        public int BuildingYear { get; set; }

        public string BuildingCategory { get; set; } = null!;

        public string BuildingClass { get; set; } = null!;

        public string BuildingName { get; set; } = null!;

        public int Levels { get; set; }

        public int Area { get; set; }

        public int FloorArea { get; set; }

        public int Flats { get; set; }
        
        public string PlaceName { get; set; } = null!;

        public int MunicipalityId { get; set; }

        public string MunicipalityName { get; set; } = null!;
    }
}