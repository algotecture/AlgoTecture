namespace AlgoTecture.Space.Domain;

public class SpaceImage
{
    public long Id { get; set; }

    public long SpaceId { get; set; }
    
    public Space Space { get; set; } = null!;

    public string? Url { get; set; }
    
    public string? Path { get; set; }
    
    public string? ContentType { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}