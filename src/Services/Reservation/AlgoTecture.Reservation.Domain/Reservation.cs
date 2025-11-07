using System.Text.Json;

namespace AlgoTecture.Reservation.Domain;

public class Reservation
{
    private Reservation() { }
    public Reservation(Guid spaceId, Guid userId,
        DateTimeOffset startUtc, DateTimeOffset endUtc, string? carNumber = null)
    {
        if (endUtc <= startUtc)
            throw new ArgumentException("End time must be later than start time.", nameof(endUtc));

        Id = Guid.NewGuid();
        SpaceId = spaceId;
        UserId = userId;
        StartUtc = startUtc;
        EndUtc = endUtc;
        Status = ReservationStatus.Pending;
        PublicId = GeneratePublicId();
        CreatedAtUtc = DateTimeOffset.UtcNow;
        var meta = new Dictionary<string, object>();
        
        if (!string.IsNullOrWhiteSpace(carNumber))
            meta["carNumber"] = NormalizeCarNumber(carNumber);

        Metadata = JsonSerializer.Serialize(meta);
    }
    
    public Guid Id { get; private set; }
    
    public Guid SpaceId { get; private set; }        
    
    public Guid UserId { get; private set; }          
    
    public DateTimeOffset StartUtc { get; private set; }
    
    public DateTimeOffset EndUtc { get; private set; }

    public ReservationStatus Status { get; private set; } 
    
    public decimal? TotalPrice { get; private set; }              

    public string? Currency { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }
    
    public DateTimeOffset? ConfirmedAtUtc { get; private set; }
    
    public DateTimeOffset? CancelledAtUtc { get; private set; }
    
    public DateTimeOffset? CompletedAtUtc { get; private set; }

    public string Metadata { get; private set; } = "{}";
    
    public int ExtensionCount { get; private set; } = 0;
    
    public DateTimeOffset? LastExtendedAtUtc { get; private set; }
    
    public string? PublicId { get; private set; }
    
    
    private static string GeneratePublicId()
    {
        var date = DateTime.UtcNow.ToString("yyyyMMdd");
        var random = Random.Shared.Next(1000, 9999);
        return $"RSV-{date}-{random}";
    }
    
    private static string NormalizeCarNumber(string number)
    {
        return number.Trim().ToUpperInvariant().Replace(" ", "").Replace("-", "");
    }
}