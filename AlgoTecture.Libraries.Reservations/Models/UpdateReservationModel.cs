﻿namespace Algotecture.Libraries.Reservations.Models;

public class UpdateReservationModel
{
    public long? ReservationId { get; set; }
    
    public long TenantUserId { get; set; }
    
    public long SpaceId { get; set; }
    
    public string? SubSpaceId { get; set; }

    public long PriceSpecificationId { get; set; }
    
    public DateTime? ReservationDateTimeUtc { get; set; }

    public DateTime? ReservationFromUtc { get; set; }
        
    public DateTime? ReservationToUtc { get; set; }

    public string? ReservationStatus { get; set; }

    public string? TotalPrice { get; set; }

    public string? Description { get; set; }
}