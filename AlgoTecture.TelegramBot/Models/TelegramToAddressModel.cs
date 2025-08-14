namespace AlgoTecture.TelegramBot.Models;

public class TelegramToAddressModel
{
    public string? FeatureId { get; set; }

    public double latitude { get; set; }

    public double longitude { get; set; }

    public string? Address { get; set; }
}