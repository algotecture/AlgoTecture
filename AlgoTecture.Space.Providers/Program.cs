using AlgoTecture.Space.Infrastructure;
using AlgoTecture.Space.Providers;

var factory = new SpaceRuntimeContextFactory();
await using var db = factory.CreateDbContext();

var kowerkDietlikonSpaces = await new KowerkDietlikonImporter().ImportAsync();

db.Spaces.AddRange(kowerkDietlikonSpaces);
await db.SaveChangesAsync();

Console.WriteLine("Import completed");