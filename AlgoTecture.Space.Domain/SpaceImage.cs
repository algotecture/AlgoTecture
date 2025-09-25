namespace AlgoTecture.Space.Domain;

public class SpaceImage
{
    public Guid Id { get; set; }

    public Guid SpaceId { get; set; }
    
    public Space Space { get; set; } = null!;

    public string? Url { get; set; }
    
    public string? Path { get; set; }
    
    public string? ContentType { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}