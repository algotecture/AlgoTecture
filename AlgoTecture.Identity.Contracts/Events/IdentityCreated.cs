using AlgoTecture.Shared.Contracts;

namespace AlgoTecture.Identity.Contracts.Events;

public record IdentityCreated(long IdentityId, string Provider, string ProviderUserId, string? ProviderUserFullName)
    : IIntegrationEvent
{
    public long Id { get; init; }
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
