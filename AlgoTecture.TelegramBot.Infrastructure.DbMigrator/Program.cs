using AlgoTecture.TelegramBot.Infrastructure;

Console.WriteLine($"{nameof(AlgoTecture.TelegramBot.Infrastructure)} has been started");

var context = new TelegramAccountDesignTimeContextFactory().CreateDbContext([]);
await context.Database.EnsureCreatedAsync();

Console.WriteLine("done");

return 1;