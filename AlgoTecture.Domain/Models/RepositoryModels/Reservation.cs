namespace AlgoTecture.Domain.Models.RepositoryModels;

public class Reservation
{
    public long Id { get; set; }
    
    public long TenantUserId { get; set; }
        
    public User TenantUser { get; set; }
    
    public long SpaceId { get; set; }

    public Space Space { get; set; }
    
    public string SubSpaceId { get; set; }
    
    public string TotalPrice { get; set; }

    public long PriceSpecificationId { get; set; }

    public PriceSpecification PriceSpecification { get; set; }

    public DateTime? ReservationDateTime { get; set; }

    public DateTime? ReservationFrom { get; set; }
        
    public DateTime? ReservationTo { get; set; }

    public string ReservationStatus { get; set; }
}