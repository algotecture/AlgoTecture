using AlgoTecture.Reservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.Reservation.Infrastructure;

public class ReservationDesignTimeContextFactory : IDesignTimeDbContextFactory<ReservationDbContext>
{
    public ReservationDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        Console.WriteLine($"Using environment: {environment}");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) 
            .AddEnvironmentVariables() 
            .Build();
        
        var connectionString = args.Length > 0 ? args[0] : configuration.GetConnectionString("AlgoTecturePostgresReservation");
        
        var optionsBuilder = new DbContextOptionsBuilder<ReservationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new ReservationDbContext(optionsBuilder.Options);
    } 
}