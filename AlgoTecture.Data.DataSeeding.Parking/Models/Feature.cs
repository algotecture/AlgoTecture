namespace AlgoTecture.Data.DataSeeding.Parking.Models;

public class Feature
{
    public string type { get; set; } = null!;

    public Geometry geometry { get; set; } = null!;

    public Properties properties { get; set; } = null!;
}