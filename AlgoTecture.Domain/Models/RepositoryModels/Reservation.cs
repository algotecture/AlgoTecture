namespace Algotecture.Domain.Models.RepositoryModels;

public class Reservation
{
    public long Id { get; set; }
    
    public string? Description { get; set; }
    
    public long TenantUserId { get; set; }
        
    public User? TenantUser { get; set; }

    public long SpaceId { get; set; }

    public Space? Space { get; set; }

    public string? SubSpaceId { get; set; }
    
    public string? TotalPrice { get; set; }

    public long PriceSpecificationId { get; set; }

    public PriceSpecification? PriceSpecification { get; set; }

    public DateTime? ReservationDateTimeUtc { get; set; }

    public DateTime? ReservationFromUtc { get; set; }
        
    public DateTime? ReservationToUtc { get; set; }

    public string? ReservationStatus { get; set; }

    public string? ReservationUniqueIdentifier { get; set; }
}