using Newtonsoft.Json.Linq;

namespace Algotecture.Data.DataSeeding.Parking.Models;

public class Geometry
{
    public string type { get; set; } = null!;
    public List<JToken> coordinates { get; set; } = null!;
}