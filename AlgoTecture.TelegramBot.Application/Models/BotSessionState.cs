namespace AlgoTecture.TelegramBot.Application.Models;

public class BotSessionState
{
    public int MessageId { get; set; }
    
    public ReservationDraft CurrentReservation { get; set; } = new();
}