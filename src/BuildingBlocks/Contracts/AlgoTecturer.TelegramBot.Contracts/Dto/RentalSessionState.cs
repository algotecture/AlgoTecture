namespace AlgoTecturer.TelegramBot.Contracts.Dto;

public class RentalSessionState
{
    public int SpaceTypeId { get; set; }
    
    public long SpaceId { get; set; }

    public string? SpaceName { get; set; }
    
    public string? SpaceAddress { get; set; }
    
    public DateTime? StartRent { get; set; }

    public DateTime? EndRent { get; set; }
}