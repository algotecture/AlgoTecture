using Algotecture.Shared.Contracts;

namespace Algotecture.User.Contracts.Events;

public record UserCreated(long UserId, long IdentityId) : IIntegrationEvent
{
    public long Id { get; init; }
    
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}