using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AlgoTecture.Libraries.GeoAdminSearch;

public static class GeoAdminLibraryUsage
{
    public static IServiceCollection UseGeoAdminLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));
    
        serviceCollection.TryAddTransient<IGeoAdminSearcher, GeoAdminSearcher>();
        return serviceCollection;
    }
}