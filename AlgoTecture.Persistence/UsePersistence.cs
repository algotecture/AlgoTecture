using System.Diagnostics.CodeAnalysis;
using AlgoTecture.Persistence.Core.Interfaces;
using AlgoTecture.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Persistence;

public static class UsePersistence
{
    public static IServiceCollection UsePersistenceLibrary([NotNull] this IServiceCollection serviceCollection)
    {

        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        return serviceCollection;

    }

}