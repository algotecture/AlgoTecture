using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace AlgoTecture.Space.Domain;

public class Space
{
    public long Id { get; set; }
    
    public long? ParentId { get; set; }
    
    public Space? Parent { get; set; }
    
    public ICollection<Space> Children { get; set; } = new List<Space>();

    public int SpaceTypeId { get; set; }

    public SpaceType SpaceType { get; set; } = null!;

    public string? SpaceAddress { get; set; }

    public Point? Location { get; set; }

    public decimal? Area { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? SpaceProperties { get; set; }
    
    
    public string? DataSource { get; set; }
    
    public ICollection<SpaceImage> Images { get; set; } = new List<SpaceImage>();

    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;

    public bool IsDeleted { get; set; }
}