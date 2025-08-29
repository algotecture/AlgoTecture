using System.Diagnostics.CodeAnalysis;
using Algotecture.Libraries.Users.Implementations;
using Algotecture.Libraries.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Algotecture.Libraries.Users;

public static class UseUser
{
    public static IServiceCollection UseUserLibrary([NotNull] this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddTransient<IUserService, UserService>();
        serviceCollection.AddTransient<IBearerAuthenticator, BearerAuthenticator>();
        serviceCollection.AddTransient<IUserCredentialsValidator, UserCredentialsValidator>();
        serviceCollection.AddTransient<IPasswordEncryptor, PasswordEncryptor>();

        return serviceCollection;
    }  
}