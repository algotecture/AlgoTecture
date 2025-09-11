namespace Algotecture.TelegramBot.Models;

public class TelegramToAddressModel
{
    public string? FeatureId { get; set; }

    public double latitude { get; set; }

    public double longitude { get; set; }

    public string? Address { get; set; }
    
    public double OriginalAddressLatitude { get; set; }

    public double OriginalAddressLongitude { get; set; }
}