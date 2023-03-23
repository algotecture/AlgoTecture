namespace AlgoTecture.TelegramBot.Models;

public class ReservationToTelegramOut
{
    public long Id { get; set; }

    public string DateTimeFrom { get; set; }
    
    public string DateTimeTo { get; set; }

    public string TotlaPrice { get; set; }

    public string PriceCurrency { get; set; }

    public string Description { get; set; }
}