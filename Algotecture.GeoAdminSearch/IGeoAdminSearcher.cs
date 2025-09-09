using Algotecture.GeoAdminSearch.Models;
using Algotecture.GeoAdminSearch.Models.GeoAdminModels;

namespace Algotecture.GeoAdminSearch
{
    public interface IGeoAdminSearcher
    {
        Task<GeoAdminBuilding> GetBuildingModel(GeoAdminSearchBuildingModel geoAdminSearchBuildingModel);

        Task<IEnumerable<Attrs>> GetAddress(string baseEndpoint, string term, int limitAddress = 5);
    }
}