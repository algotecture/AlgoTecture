using Algotecture.Shared.Contracts;

namespace Algotecture.Identity.Contracts.Events;

public record IdentityCreated(long IdentityId, string Provider, string ExternalId)
    : IIntegrationEvent
{
    public long Id { get; init; }
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
