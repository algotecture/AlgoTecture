using AlgoTecture.Shared.Contracts;

namespace AlgoTecture.User.Contracts.Events;

public record UserCreated(long UserId, long IdentityId, string Provider, string ProviderUserId) : IIntegrationEvent
{
    public long Id { get; init; }
    
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}