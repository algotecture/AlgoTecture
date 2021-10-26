using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoTecture.Assistants;
using AlgoTecture.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoTecture.Controllers
{
    [Route("[controller]")]
    public class GeoAdminSearchController : Controller
    {
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
        
        [HttpGet("SearchTargetBuilding")]
        public async Task<ActionResult<IEnumerable<Attrs>>> SearchTargetBuilding([FromQuery] string latitude, string longitude)
        {
            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude)) return BadRequest();

            
            // var responseFromServer = await HttpWebRequestAssistant.GetResponse(baseUrl);
            //
            // var addressResults = JsonConvert.DeserializeObject<GeoadminApiSearch>(responseFromServer);
            // var labels = addressResults?.results.Select(x => x.attrs);
            //
            // return labels;
            throw new NotImplementedException();
        }
    }
}