using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.PriceSpecification;

public static class UsePriceSpecification
{
    public static IServiceCollection UsePriceSpecificationLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IPriceSpecificationService, PriceSpecificationService>();

        return serviceCollection;
    }
}