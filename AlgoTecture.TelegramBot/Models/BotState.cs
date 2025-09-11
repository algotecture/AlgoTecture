namespace AlgoTecture.TelegramBot.Models;

public class BotState
{
    public int UtilizationTypeId { get; set; }

    public int MessageId { get; set; }

    public long SpaceId { get; set; }

    public string? SpaceName { get; set; }
    
    public DateTime? StartRent { get; set; }

    public DateTime? EndRent { get; set; }

    public string? UtilizationName { get; set; }
    
    public string? SpaceAddress { get; set; }
    
}