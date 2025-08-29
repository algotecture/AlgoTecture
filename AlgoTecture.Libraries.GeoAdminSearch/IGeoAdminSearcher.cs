using Algotecture.Libraries.GeoAdminSearch.Models;
using Algotecture.Libraries.GeoAdminSearch.Models.GeoAdminModels;

namespace Algotecture.Libraries.GeoAdminSearch
{
    public interface IGeoAdminSearcher
    {
        Task<GeoAdminBuilding> GetBuildingModel(GeoAdminSearchBuildingModel geoAdminSearchBuildingModel);

        Task<IEnumerable<Attrs>> GetAddress(string term);
    }
}