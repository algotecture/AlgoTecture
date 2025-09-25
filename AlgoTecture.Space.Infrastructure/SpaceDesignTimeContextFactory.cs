using System.IO;
using AlgoTecture.Space.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.Space.Infrastructure;

public class SpaceDesignTimeContextFactory : IDesignTimeDbContextFactory<SpaceDbContext>
{
    public SpaceDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
        
        var connectionString = args.Length > 0 ? args[0] : configuration.GetConnectionString("AlgoTecturePostgresSpaceTest");
        
        var optionsBuilder = new DbContextOptionsBuilder<SpaceDbContext>();
        optionsBuilder.UseNpgsql(connectionString,
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(SpaceDbContext).Assembly.FullName);
                sqlOptions.UseNetTopologySuite();
            });

        return new SpaceDbContext(optionsBuilder.Options);
    } 
}