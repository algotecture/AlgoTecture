using AlgoTecture.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.Identity.Infrastructure;

public class IdentityRuntimeContextFactory : IDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
        
        ConfigureOptions(optionsBuilder);
        
        return new IdentityDbContext(optionsBuilder.Options);
    }
    
    public static void ConfigureOptions(DbContextOptionsBuilder<IdentityDbContext> optionsBuilder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        Console.WriteLine($"Using environment: {environment}");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) 
            .AddEnvironmentVariables() 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresIdentity");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName));
    }
}