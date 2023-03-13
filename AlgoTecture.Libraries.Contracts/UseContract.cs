using System.Diagnostics.CodeAnalysis;
using AlgoTecture.Libraries.Contracts.Implementations;
using AlgoTecture.Libraries.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.Contracts;

public static class UseContract
{
    public static IServiceCollection UseContractLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IContractService, ContractService>();

        return serviceCollection;
    }
}