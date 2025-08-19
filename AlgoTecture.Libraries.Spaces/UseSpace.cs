using AlgoTecture.Data.Images;
using AlgoTecture.Libraries.Spaces.Implementations;
using AlgoTecture.Libraries.Spaces.Interfaces;
using GeoDistanceLib;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.Spaces;

public static class UseSpace
{
        public static IServiceCollection UseSpaceLibrary([NotNull] this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddTransient<ISpaceGetter, SpaceGetter>();
            serviceCollection.AddTransient<ISpaceService, SpaceService>();
            serviceCollection.AddTransient<ISpaceImageService, SpaceImageService>();
            serviceCollection.AddTransient<IDistanceCalculator, DistanceCalculator>();
            serviceCollection.UseImageLibrary();
            
            return serviceCollection;
        }  
}