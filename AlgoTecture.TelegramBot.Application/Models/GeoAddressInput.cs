using NetTopologySuite.Geometries;

namespace AlgoTecture.TelegramBot.Application.Models;

public class GeoAddressInput
{
    public string? FeatureId { get; set; }
    
    public Point Location { get; set; } = default!;
    
    public string? NormalizedAddress { get; set; }
    
    public Point OriginalInput { get; set; } = default!;
}