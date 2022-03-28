using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.EfCli;

public static class UseEfCli
{
    public static IServiceCollection UseEfCliLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddDbContext<ApplicationDbContext>();
        
        return serviceCollection;
    }
}