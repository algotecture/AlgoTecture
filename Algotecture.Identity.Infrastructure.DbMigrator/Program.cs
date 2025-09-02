using Algotecture.Identity.Infrastructure;

Console.WriteLine($"{nameof(Algotecture.Identity.Infrastructure)} has been started");

var context = new IdentityDesignTimeContextFactory().CreateDbContext([]);
await context.Database.EnsureCreatedAsync();

Console.WriteLine("done");

return 1;

