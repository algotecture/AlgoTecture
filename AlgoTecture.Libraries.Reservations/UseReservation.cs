using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.Reservations;

public static class UseReservation
{
    public static IServiceCollection UseReservationLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IReservationService, ReservationService>();

        return serviceCollection;
    }
}