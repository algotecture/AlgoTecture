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
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true) 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresUserTest");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName));
    }
}