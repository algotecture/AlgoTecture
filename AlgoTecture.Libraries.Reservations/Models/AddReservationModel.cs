namespace AlgoTecture.Libraries.Reservations.Models;

public class AddReservationModel
{
    public long TenantUserId { get; set; }
    
    public long SpaceId { get; set; }
    
    public string? SubSpaceId { get; set; }

    public DateTime? ReservationDateTimeUtc { get; set; }

    public DateTime ReservationFromUtc { get; set; }
        
    public DateTime ReservationToUtc { get; set; }

    public string? Description { get; set; }
}