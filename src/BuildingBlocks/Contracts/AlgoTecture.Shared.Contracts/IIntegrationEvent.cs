using MassTransit;

namespace AlgoTecture.Shared.Contracts;

[ExcludeFromTopology]
public interface IIntegrationEvent
{
    long Id { get; }
    DateTime OccurredOn { get; }
}