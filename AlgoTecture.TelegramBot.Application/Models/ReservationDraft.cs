namespace AlgoTecture.TelegramBot.Application.Models;

public class ReservationDraft
{
    public Guid SelectedSpaceId { get; set; }

    public int SelectedSpaceTypeId { get; set; }
    
    public DateTime? PendingStartRentUtc { get; set; }
    public DateTime? PendingEndRentUtc { get; set; }
    
    public string? SpaceName { get; set; }
    
    public string? SpaceAddress { get; set; }

    public GeoAddressInput GeoAddressInput { get; set; }

    public bool IsComplete =>
        SelectedSpaceId != default &&
        PendingStartRentUtc.HasValue &&
        PendingEndRentUtc.HasValue &&
        PendingEndRentUtc > PendingStartRentUtc;
}