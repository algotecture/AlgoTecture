namespace AlgoTecture.Data.DataSeeding.Parking.Models;

public class Feature
{
    public string type { get; set; }
    
    public Geometry geometry { get; set; }
    
    public Properties properties { get; set; }
}