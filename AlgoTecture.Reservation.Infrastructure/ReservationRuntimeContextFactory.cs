using AlgoTecture.Reservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.Reservation.Infrastructure;

public class ReservationRuntimeContextFactory : IDbContextFactory<ReservationDbContext>
{
    public ReservationDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ReservationDbContext>();
        
        ConfigureOptions(optionsBuilder);
        
        return new ReservationDbContext(optionsBuilder.Options);
    }
    
    public static void ConfigureOptions(DbContextOptionsBuilder<ReservationDbContext> optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true) 
            .Build();

        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresReservationTest");
        
        optionsBuilder.UseNpgsql(connectionString, 
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(ReservationDbContext).Assembly.FullName));
    }
}