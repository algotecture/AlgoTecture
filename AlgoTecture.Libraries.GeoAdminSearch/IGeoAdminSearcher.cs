using AlgoTecture.Libraries.GeoAdminSearch.Models;
using AlgoTecture.Libraries.GeoAdminSearch.Models.GeoAdminModels;

namespace AlgoTecture.Libraries.GeoAdminSearch
{
    public interface IGeoAdminSearcher
    {
        Task<GeoAdminBuilding> GetBuildingModel(GeoAdminSearchBuildingModel geoAdminSearchBuildingModel);

        Task<IEnumerable<Attrs>> GetAddress(string term);
    }
}