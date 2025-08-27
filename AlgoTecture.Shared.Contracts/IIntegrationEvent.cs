namespace AlgoTecture.Shared.Contracts;

public interface IIntegrationEvent
{
    long Id { get; }
    DateTime OccurredOn { get; }
}