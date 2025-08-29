namespace Algotecture.Data.DataSeeding.Parking.Models;

public class Root
{
    public string type { get; set; } = null!;

    public string name { get; set; } = null!;

    public List<Feature> features { get; set; } = null!;
}