using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.GeoAdminSearch;

public static class UseGeoAdminSearch
{
    public static IServiceCollection UseGeoAdminSearchLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IGeoAdminSearcher, GeoAdminSearcher>();
            
        return serviceCollection;
    }  
}