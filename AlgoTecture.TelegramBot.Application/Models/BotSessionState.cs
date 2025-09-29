namespace AlgoTecture.TelegramBot.Application.Models;

public class BotSessionState
{
    public int LastMessageId { get; set; }  
    
    public Guid? SelectedSpaceId { get; set; }
    
    public int? SelectedSpaceTypeId { get; set; }
    
    public DateTime? PendingStartRentUtc { get; set; }
    
    public DateTime? PendingEndRentUtc { get; set; } 
}