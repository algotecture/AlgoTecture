namespace Algotecture.Space.Domain;

public class Space
{
    public long Id { get; set; }
    
    public int? ParentId { get; set; }
    
    public Space? Parent { get; set; }
    
    public ICollection<Space> Children { get; set; } = new List<Space>();

    public int SpaceTypeId { get; set; }

    public SpaceType? SpaceType { get; set; }

    public string? SpaceAddress { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
    
    public long OwnerId { get; set; }

    public double Area { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? SpaceProperties { get; set; }
    
    public ICollection<SpaceImage> Images { get; set; } = new List<SpaceImage>();

    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
}