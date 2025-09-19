using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.TelegramBot.Infrastructure;

public class TelegramAccountRuntimeContextFactory : IDbContextFactory<TelegramAccountDbContext>
{
    public TelegramAccountDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TelegramAccountDbContext>();
        
        ConfigureOptions(optionsBuilder);
        
        return new TelegramAccountDbContext(optionsBuilder.Options);
    }
    
    public static void ConfigureOptions(DbContextOptionsBuilder<TelegramAccountDbContext> optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true) 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresTelegramAccountTest");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(TelegramAccountDbContext).Assembly.FullName));
    }
}