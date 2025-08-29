using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Algotecture.Libraries.PriceSpecifications;

public static class UsePriceSpecification
{
    public static IServiceCollection UsePriceSpecificationLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IPriceSpecificationService, PriceSpecificationService>();
        serviceCollection.AddTransient<IPriceCalculator, PriceCalculator>();

        return serviceCollection;
    }
}