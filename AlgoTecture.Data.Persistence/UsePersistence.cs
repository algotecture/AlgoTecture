using System.Diagnostics.CodeAnalysis;
using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Data.Persistence.Data;
using Algotecture.Data.Persistence.Ef;
using Microsoft.Extensions.DependencyInjection;

namespace Algotecture.Data.Persistence;

public static class UsePersistence
{
    public static IServiceCollection UsePersistenceLibrary([NotNull] this IServiceCollection serviceCollection)
    {

        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddDbContext<ApplicationDbContext>();

        return serviceCollection;
    }

}