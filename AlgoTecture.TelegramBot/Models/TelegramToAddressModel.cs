namespace AlgoTecture.TelegramBot.Models;

public class TelegramToAddressModel
{
    public string? FeatureId { get; set; }

    public string latitude { get; set; }

    public string longitude { get; set; }

    public string? Address { get; set; }
    
    public string OriginalAddressLatitude { get; set; }

    public string OriginalAddressLongitude { get; set; }
}