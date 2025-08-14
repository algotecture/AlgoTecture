using Newtonsoft.Json.Linq;

namespace AlgoTecture.Data.DataSeeding.Parking.Models;

public class Geometry
{
    public string type { get; set; }
    public List<JToken> coordinates { get; set; }
}