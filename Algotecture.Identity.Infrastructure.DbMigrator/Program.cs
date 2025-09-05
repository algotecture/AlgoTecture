using Algotecture.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

Console.WriteLine($"{nameof(Algotecture.Identity.Infrastructure)} has been started");

var context = new IdentityDesignTimeContextFactory().CreateDbContext([]);
await context.Database.EnsureCreatedAsync();
await context.Database.MigrateAsync();

Console.WriteLine("done");

return 1;

