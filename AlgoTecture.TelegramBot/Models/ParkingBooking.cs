namespace AlgoTecture.TelegramBot.Models;

public class ParkingBooking
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Address { get; set; } = string.Empty;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string CarNumber { get; set; } = string.Empty;
    public string ParkingType { get; set; } = "стандартная";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "active"; // active, cancelled, completed
}