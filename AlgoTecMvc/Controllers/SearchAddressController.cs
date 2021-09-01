using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AlgoTecMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoTecMvc.Controllers
{
    public class SearchAddressController : Controller
    {
        public async Task<JsonResult> GeoAdminSearch([FromQuery]string term)
        {
            if (string.IsNullOrEmpty(term)) return null;
            
            var baseUrl = $"https://api3.geo.admin.ch/rest/services/api/SearchServer?searchText={term}&type=locations&origins=address&limit=10";

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(baseUrl);
            
                var response = (HttpWebResponse) await request.GetResponseAsync();

                if (response.StatusCode != HttpStatusCode.OK) return null;
                
                var data = response.GetResponseStream();
            
                var reader = new StreamReader(data);
            
                var responseFromServer = await reader.ReadToEndAsync();
            
                response.Close();
                
                var addressResults = JsonConvert.DeserializeObject<GeoadminApiSearch>(responseFromServer);
                var labels = addressResults?.results.Select(x=>x.attrs);
                
                return Json(labels?.ToList());
            }
            catch (WebException ex)
            {
                //logging
                throw;
            }
        }
    }
}