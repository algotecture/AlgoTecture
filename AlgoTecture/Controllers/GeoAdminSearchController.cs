using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoTecture.Assistants;
using AlgoTecture.Interfaces;
using AlgoTecture.Models.Dto;
using AlgoTecture.Models.GeoAdminModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoTecture.Controllers
{
    [Route("[controller]")]
    public class GeoAdminSearchController : Controller
    {
        private readonly IGeoAdminSearcher _geoAdminSearcher;

        public GeoAdminSearchController(IGeoAdminSearcher geoAdminSearcher)
        {
            _geoAdminSearcher = geoAdminSearcher ?? throw new ArgumentNullException(nameof(geoAdminSearcher));
        }

        [HttpGet("SearchAddress")]
        public async Task<ActionResult<IEnumerable<Attrs>>> SearchAddress([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term)) return BadRequest();

            var baseUrl = $"https://api3.geo.admin.ch/rest/services/api/SearchServer?searchText={term}&type=locations&origins=address&limit=10";

            var responseFromServer = await HttpWebRequestAssistant.GetResponse(baseUrl);

            var addressResults = JsonConvert.DeserializeObject<GeoadminApiSearch>(responseFromServer);
            var labels = addressResults?.results.Select(x => x.attrs);

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