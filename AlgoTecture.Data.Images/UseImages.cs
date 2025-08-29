using System.Diagnostics.CodeAnalysis;
using Algotecture.Data.Images.Implementations;
using Algotecture.Data.Images.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Algotecture.Data.Images;

public static class UseImage
{
        public static IServiceCollection UseImageLibrary([NotNull] this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddTransient<IImageUploader, ImageUploader>();
            serviceCollection.AddTransient<IImageService, ImageService>();

            return serviceCollection;
        }  
}