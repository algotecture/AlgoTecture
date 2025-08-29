using System.Collections.Generic;
using System.Threading.Tasks;
using Algotecture.Libraries.GeoAdminSearch;
using Algotecture.Libraries.GeoAdminSearch.Models;
using Algotecture.Libraries.GeoAdminSearch.Models.GeoAdminModels;
using Microsoft.AspNetCore.Mvc;

namespace Algotecture.WebApi.Controllers
{
    [Route("[controller]")]
    public class GeoAdminSearchController : Controller
    {
        private readonly IGeoAdminSearcher _geoAdminSearcher;

        public GeoAdminSearchController(IGeoAdminSearcher geoAdminSearcher)
        {
            _geoAdminSearcher = geoAdminSearcher;
        }

        [HttpGet("SearchAddress")]
        public async Task<ActionResult<IEnumerable<Attrs>>> SearchAddress([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term)) return BadRequest();

            var labels = await _geoAdminSearcher.GetAddress(term);

            return new ActionResult<IEnumerable<Attrs>>(labels);
        }
        
        [HttpPost("SearchTargetBuilding")]
        public async Task<ActionResult<GeoAdminBuilding>> SearchTargetBuilding([FromBody] GeoAdminSearchBuildingModel geoAdminSearchBuildingModel)
        {
            if (geoAdminSearchBuildingModel == null) return BadRequest();

            var targetBuilding =  await _geoAdminSearcher.GetBuildingModel(geoAdminSearchBuildingModel);

            return targetBuilding;
        }
    }
}