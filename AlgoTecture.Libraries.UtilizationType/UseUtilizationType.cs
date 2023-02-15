using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.UtilizationType;

public static class UseUtilizationType
{
    public static IServiceCollection UseUtilizationTypeLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IUtilizationTypeGetter, UtilizationTypeGetter>();
        
        return serviceCollection;
    }  
}