using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Domain.Models.Dto;

public class SpaceWithProperty
{
    public long Id { get; set; }

    public int UtilizationTypeId { get; set; }

    public UtilizationType? UtilizationType { get; set; }

    public string SpaceAddress { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }
    
    public SpaceProperty? SpaceProperty { get; set; }
}