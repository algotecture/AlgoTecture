using AlgoTecture.Identity.Infrastructure;
using AlgoTecture.Identity.Infrastructure.Persistence;
using AlgoTecture.Shared.Contracts;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Xunit;

namespace AlgoTecture.Identity.Tests.Fixtures;

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<PostgresContainerFixture> { }

public class DatabaseFixture : IAsyncLifetime
{
    private readonly PostgresContainerFixture _pg;
    private Respawner _respawner = default!;
    
    public string ConnectionString => _pg.ConnectionString;

    public DatabaseFixture(PostgresContainerFixture pg) => _pg = pg;

    public async Task InitializeAsync()
    {
        var context = new IdentityDesignTimeContextFactory()
            .CreateDbContext([_pg.ConnectionString]);

        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();

        await using var conn = new NpgsqlConnection(_pg.ConnectionString);
        await conn.OpenAsync();

        _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }

    public async Task ResetDatabaseAsync()
    {
        await using var conn = new NpgsqlConnection(_pg.ConnectionString);
        await conn.OpenAsync();
        await _respawner.ResetAsync(conn);
    }

    public async Task SeedTestData(IdentityDbContext context)
    {
        var faker = new Faker<Domain.Identity>()
            .RuleFor(i => i.Provider, f => f.PickRandom<AuthProvider>().ToDbValue())
            .RuleFor(i => i.ProviderUserId, f => f.Random.Guid().ToString());

        await context.Identities.AddRangeAsync(faker.Generate(10));
        await context.SaveChangesAsync();
    }
    
    public IdentityDbContext GetIdentityDbContext()
    {
        return new IdentityDesignTimeContextFactory().CreateDbContext([_pg.ConnectionString!]);
    }
    
    public Task DisposeAsync() => Task.CompletedTask;
}