using System.Diagnostics.CodeAnalysis;
using AlgoTecture.Libraries.Contract.Implementations;
using AlgoTecture.Libraries.Contract.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.Contract;

public static class UseContract
{
    public static IServiceCollection UseContractLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IContractService, ContractService>();

        return serviceCollection;
    }
}