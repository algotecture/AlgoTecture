using Algotecture.User.Infrastructure;

Console.WriteLine($"{nameof(Algotecture.User.Infrastructure)} has been started");

var context = new UserDesignTimeContextFactory().CreateDbContext([]);
await context.Database.EnsureCreatedAsync();

Console.WriteLine("done");

return 1;

