namespace AlgoTecture.TelegramBot.Application.Models;

public record GeoAdminSettings
{
    public string GeoAdminBaseUrl { get; init; } = default!;
}