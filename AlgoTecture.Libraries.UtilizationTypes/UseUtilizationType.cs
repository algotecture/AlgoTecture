using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Algotecture.Libraries.UtilizationTypes;

public static class UseUtilizationType
{
    public static IServiceCollection UseUtilizationTypeLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IUtilizationTypeGetter, UtilizationTypeGetter>();
        
        return serviceCollection;
    }  
}