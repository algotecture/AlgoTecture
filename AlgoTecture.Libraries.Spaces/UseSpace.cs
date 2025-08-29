using Algotecture.Data.Images;
using Algotecture.Libraries.Spaces.Implementations;
using Algotecture.Libraries.Spaces.Interfaces;
using GeoDistanceLib;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Algotecture.Libraries.Spaces;

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