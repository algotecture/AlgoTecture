using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.TelegramBot.Infrastructure;

public class TelegramAccountDesignTimeContextFactory : IDesignTimeDbContextFactory<TelegramAccountDbContext>
{
    public TelegramAccountDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true) 
            .Build();
        
        var connectionString = args.Length > 0 ? args[0] : configuration.GetConnectionString("AlgoTecturePostgresTelegramAccountTest");
        
        var optionsBuilder = new DbContextOptionsBuilder<TelegramAccountDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TelegramAccountDbContext(optionsBuilder.Options);
    } 
}