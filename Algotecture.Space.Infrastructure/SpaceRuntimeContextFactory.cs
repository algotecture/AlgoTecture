using Algotecture.Space.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Algotecture.Space.Infrastructure;

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
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true) 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgotecturePostgresSpaceTest");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(SpaceDbContext).Assembly.FullName));
    }
}