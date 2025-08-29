namespace Algotecture.Domain.Models.RepositoryModels;

public class PriceSpecification
{
    public long Id { get; set; }
    
    public long SpaceId { get; set; }

    public Space? Space { get; set; }
    
    public string? SubSpaceId { get; set; }

    public string PricePerTime { get; set; } = null!;

    public string PriceCurrency { get; set; } = null!;

    public string UnitOfTime { get; set; } = null!;

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidThrough { get; set; }
}