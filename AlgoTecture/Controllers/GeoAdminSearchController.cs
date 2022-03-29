using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.GeoAdminSearch.Models;
using AlgoTecture.Libraries.GeoAdminSearch.Models.GeoAdminModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace AlgoTecture.Controllers
{
    [Route("[controller]")]
    [DependsOn(typeof(GeoAdminSearcher))]
    public class GeoAdminSearchController : Controller
    {
        private readonly GeoAdminSearcher _geoAdminSearcher;

        public GeoAdminSearchController(GeoAdminSearcher geoAdminSearcher)
        {
            _geoAdminSearcher = geoAdminSearcher ?? throw new ArgumentNullException(nameof(geoAdminSearcher));
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