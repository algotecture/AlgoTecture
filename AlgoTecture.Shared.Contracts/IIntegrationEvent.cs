using MassTransit;

namespace Algotecture.Shared.Contracts;

[ExcludeFromTopology]
public interface IIntegrationEvent
{
    long Id { get; }
    DateTime OccurredOn { get; }
}