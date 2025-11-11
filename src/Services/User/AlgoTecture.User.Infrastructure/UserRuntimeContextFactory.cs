using AlgoTecture.User.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.User.Infrastructure;

public class UserRuntimeContextFactory : IDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
        
        ConfigureOptions(optionsBuilder);
        
        return new UserDbContext(optionsBuilder.Options);
    }
    
    public static void ConfigureOptions(DbContextOptionsBuilder<UserDbContext> optionsBuilder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        Console.WriteLine($"Using environment: {environment}");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) 
            .AddEnvironmentVariables() 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresUser");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName));
    }
}