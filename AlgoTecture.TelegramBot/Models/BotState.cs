namespace AlgoTecture.TelegramBot.Models;

public class BotState
{
    public int UtilizationTypeId { get; set; }

    public int MessageId { get; set; }

    public long SpaceId { get; set; }
    
    public DateTime? StartRent { get; set; }

    public DateTime? EndRent { get; set; }
}