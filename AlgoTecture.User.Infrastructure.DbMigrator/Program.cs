using AlgoTecture.User.Infrastructure;

Console.WriteLine($"{nameof(AlgoTecture.User.Infrastructure)} has been started");

var context = new UserDesignTimeContextFactory().CreateDbContext([]);
await context.Database.EnsureCreatedAsync();

Console.WriteLine("done");

return 1;

