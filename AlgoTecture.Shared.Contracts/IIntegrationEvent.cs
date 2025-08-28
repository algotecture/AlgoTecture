namespace Algotecture.Shared.Contracts;

public interface IIntegrationEvent
{
    long Id { get; }
    DateTime OccurredOn { get; }
}