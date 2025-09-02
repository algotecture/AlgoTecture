using Algotecture.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Algotecture.Identity.Infrastructure;

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
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true) 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgotecturePostgresIdentityTest");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName));
    }
}