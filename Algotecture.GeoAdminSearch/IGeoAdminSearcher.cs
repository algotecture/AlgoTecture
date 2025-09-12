using AlgoTecture.GeoAdminSearch.Models;
using AlgoTecture.GeoAdminSearch.Models.GeoAdminModels;

namespace AlgoTecture.GeoAdminSearch
{
    public interface IGeoAdminSearcher
    {
        Task<GeoAdminBuilding> GetBuildingModel(GeoAdminSearchBuildingModel geoAdminSearchBuildingModel);

        Task<IEnumerable<Attrs>> GetAddress(string baseEndpoint, string term, int limitAddress = 5);
    }
}