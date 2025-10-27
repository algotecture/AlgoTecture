namespace AlgoTecture.TelegramBot.Application.Models;

public class ReservationDraft
{
    public Guid SelectedSpaceId { get; set; }

    public int SelectedSpaceTypeId { get; set; }
    
    public DateTimeOffset? PendingStartRentLocal { get; set; }
    public DateTimeOffset? PendingEndRentLocal { get; set; }
    
    public string SpaceTimeZone { get; set; } = "Europe/Zurich";
    
    public string? SpaceName { get; set; }
    
    public string? SpaceAddress { get; set; }

    public GeoAddressInput GeoAddressInput { get; set; }

    public string? SelectedCarNumber { get; set; }

    public bool IsComplete =>
        SelectedSpaceId != default &&
        PendingStartRentLocal.HasValue &&
        PendingEndRentLocal.HasValue &&
        PendingEndRentLocal > PendingStartRentLocal;
}