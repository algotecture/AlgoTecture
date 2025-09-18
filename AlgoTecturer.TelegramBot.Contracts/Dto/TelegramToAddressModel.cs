namespace AlgoTecturer.TelegramBot.Contracts.Dto;

public class TelegramToAddressModel
{
    public string? FeatureId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string? Address { get; set; }
    
    public double OriginalAddressLatitude { get; set; }

    public double OriginalAddressLongitude { get; set; }
}