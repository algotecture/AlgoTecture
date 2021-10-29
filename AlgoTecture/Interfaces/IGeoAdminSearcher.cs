using System.Threading.Tasks;
using AlgoTecture.Models;
using AlgoTecture.Models.Dto;
using AlgoTecture.Models.GeoAdminModels;

namespace AlgoTecture.Interfaces
{
    public interface IGeoAdminSearcher
    {
        Task<GeoAdminBuilding> GetBuildingModel(GeoAdminSearchBuildingModel geoAdminSearchBuildingModel);
    }
}