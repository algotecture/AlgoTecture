using System.Diagnostics.CodeAnalysis;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Data.Persistence;

public static class UsePersistence
{
    public static IServiceCollection UsePersistenceLibrary([NotNull] this IServiceCollection serviceCollection)
    {

        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        return serviceCollection;

    }

}