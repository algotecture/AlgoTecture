using System.IO;
using Algotecture.Space.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Algotecture.Space.Infrastructure;

public class SpaceDesignTimeContextFactory : IDesignTimeDbContextFactory<SpaceDbContext>
{
    public SpaceDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true) 
            .Build();
        
        var connectionString = args.Length > 0 ? args[0] : configuration.GetConnectionString("AlgotecturePostgresSpaceTest");
        
        var optionsBuilder = new DbContextOptionsBuilder<SpaceDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new SpaceDbContext(optionsBuilder.Options);
    } 
}