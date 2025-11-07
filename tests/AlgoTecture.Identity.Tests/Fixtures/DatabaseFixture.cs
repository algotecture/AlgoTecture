using AlgoTecture.Identity.Infrastructure;
using AlgoTecture.Identity.Infrastructure.Persistence;
using AlgoTecture.Shared.Contracts;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;
using Xunit;

namespace AlgoTecture.Identity.Tests.Fixtures;

[CollectionDefinition("Identity Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }

public class DatabaseFixture : IAsyncLifetime
{
    private PostgreSqlContainer _container = default!;
    private Respawner _respawner = default!;

    public string ConnectionString => _container.GetConnectionString();

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("identity_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
#if DEBUG
            .WithReuse(true)
#endif
            .Build();

        await _container.StartAsync();

        Console.WriteLine($"   Postgres started:");
        Console.WriteLine($"   Host: {_container.Hostname}");
        Console.WriteLine($"   Port: {_container.GetMappedPublicPort(5432)}");
        Console.WriteLine($"   Connection string: {_container.GetConnectionString()}");

        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        await using var context = new IdentityDbContext(options);
        
#if DEBUG
        await using (var conn1 = new NpgsqlConnection(ConnectionString))
        {
            await conn1.OpenAsync();
            await using var cmd = new NpgsqlCommand(
                "DROP SCHEMA IF EXISTS public CASCADE; CREATE SCHEMA public;", conn1);
            await cmd.ExecuteNonQueryAsync();
        }
#endif

        await context.Database.MigrateAsync();

        await using var conn2 = new NpgsqlConnection(ConnectionString);
        await conn2.OpenAsync();

        _respawner = await Respawner.CreateAsync(conn2, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = ["__EFMigrationsHistory"]
        });

        await conn2.CloseAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await using var conn = new NpgsqlConnection(ConnectionString);
        await conn.OpenAsync();
        await _respawner.ResetAsync(conn);
        await conn.CloseAsync();
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
        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        return new IdentityDbContext(options);
    }

    public async Task DisposeAsync()
    {
        Console.WriteLine("Disposing PostgreSQL container...");
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}