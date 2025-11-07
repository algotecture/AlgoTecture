namespace AlgoTecture.AICoreService.Application.Models;

public class ParkingReservationModel
{
    public string Action { get; set; } = string.Empty;

    public string? Address { get; set; }

    public DateTime? DateTimeFrom { get; set; }
    
    public DateTime? DateTimeTo { get; set; }

    public string? CarNumber { get; set; }
    
    public string? ParkingType { get; set; }
}