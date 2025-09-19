namespace AlgoTecturer.TelegramBot.Contracts.Dto;

public class GeoAddressDto
{
    public string? FeatureId { get; set; }
    
    public string? Address { get; set; }
    
    public GeoPoint? OriginalPoint { get; set; }
    
    public GeoPoint? NearestPoint { get; set; }
}