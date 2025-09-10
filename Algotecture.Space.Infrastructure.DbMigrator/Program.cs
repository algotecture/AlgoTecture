
using Algotecture.Space.Infrastructure;
using Microsoft.EntityFrameworkCore;

Console.WriteLine($"{nameof(Algotecture.Space.Infrastructure)} has been started");

var context = new SpaceDesignTimeContextFactory().CreateDbContext([]);
await context.Database.EnsureCreatedAsync();
await context.Database.MigrateAsync();

Console.WriteLine("done");

return 1;