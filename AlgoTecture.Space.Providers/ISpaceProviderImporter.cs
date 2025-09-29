namespace AlgoTecture.Space.Providers;

public interface ISpaceProviderImporter
{
    Task<IEnumerable<Domain.Space>> ImportAsync();
    
    string ProviderName { get; }
}