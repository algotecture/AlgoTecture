using System.IO;
using AlgoTecture.Space.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.Space.Infrastructure;

public class SpaceRuntimeContextFactory : IDbContextFactory<SpaceDbContext>
{
    public SpaceDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<SpaceDbContext>();
        
        ConfigureOptions(optionsBuilder);
        
        return new SpaceDbContext(optionsBuilder.Options);
    }
    
    public static void ConfigureOptions(DbContextOptionsBuilder<SpaceDbContext> optionsBuilder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        Console.WriteLine($"Using environment: {environment}");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) 
            .AddEnvironmentVariables() 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresSpace");

        optionsBuilder.UseNpgsql(connectionString,
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(SpaceDbContext).Assembly.FullName);
                sqlOptions.UseNetTopologySuite();
            });
    }
}