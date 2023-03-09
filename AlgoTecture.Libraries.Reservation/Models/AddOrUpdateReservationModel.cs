namespace AlgoTecture.Libraries.Reservation.Models;

public class AddOrUpdateReservationModel
{
    public long? ReservationId { get; set; }
    
    public long TenantUserId { get; set; }
    
    public long SpaceId { get; set; }
    
    public string SubSpaceId { get; set; }
    
    public string PriceCurrency { get; set; }

    public long PriceSpecificationId { get; set; }
    
    public DateTime? ReservationDateTime { get; set; }

    public DateTime? ReservationFrom { get; set; }
        
    public DateTime? ReservationTo { get; set; }

    public string ReservationStatus { get; set; }

    public string TotalPrice { get; set; }
}