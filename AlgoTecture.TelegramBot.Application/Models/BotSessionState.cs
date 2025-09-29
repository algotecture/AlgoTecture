namespace AlgoTecture.TelegramBot.Application.Models;

public class BotSessionState
{
    public int LastMessageId { get; set; }
    
    public ReservationDraft CurrentReservation { get; set; } = new();
}