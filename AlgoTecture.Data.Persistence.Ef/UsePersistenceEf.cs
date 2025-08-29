using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Algotecture.Data.Persistence.Ef;

public static class UsePersistenceEf
{
    public static IServiceCollection UsePersistenceEfLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));
        
        serviceCollection.AddDbContext<ApplicationDbContext>();

        return serviceCollection;
    }
}