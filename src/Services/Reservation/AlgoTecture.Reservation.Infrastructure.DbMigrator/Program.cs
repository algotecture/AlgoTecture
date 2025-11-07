using AlgoTecture.Reservation.Infrastructure;
using Microsoft.EntityFrameworkCore;

Console.WriteLine($"{typeof(Program).Assembly.GetName().Name} has been started");

var context = new ReservationDesignTimeContextFactory().CreateDbContext([]);
await context.Database.MigrateAsync();

Console.WriteLine("done");

return 1;