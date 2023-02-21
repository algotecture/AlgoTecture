using System.Diagnostics.CodeAnalysis;
using AlgoTecture.Libraries.User.Implementations;
using AlgoTecture.Libraries.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTecture.Libraries.User;

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