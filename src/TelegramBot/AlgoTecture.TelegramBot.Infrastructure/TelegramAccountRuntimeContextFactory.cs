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
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        Console.WriteLine($"Using environment: {environment}");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) 
            .AddEnvironmentVariables() 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresTelegramAccountTest");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(TelegramAccountDbContext).Assembly.FullName));
    }
}