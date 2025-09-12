using AlgoTecture.Identity.Infrastructure;
using AlgoTecture.Identity.Infrastructure.Persistence;
using AlgoTecture.Shared.Contracts;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Xunit;

namespace AlgoTecture.Identity.Tests;

public class DatabaseFixture : IAsyncLifetime
{
    private Respawner? _respawner;
    public IConfiguration Configuration { get; }

    private readonly string? _connectionString;

    public DatabaseFixture()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        _connectionString = Configuration.GetConnectionString("AlgoTecturePostgresIdentityTest");
        if (string.IsNullOrEmpty(_connectionString)) throw new InvalidOperationException("Connection string is null");
    }

    public async Task InitializeAsync()
    {
        var context = new IdentityDesignTimeContextFactory().CreateDbContext([_connectionString!]);

        await context.Database.EnsureCreatedAsync();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (PostgresException ex) when (ex.SqlState == "42P07")
            {
                await context.Database.ExecuteSqlRawAsync(
                    "INSERT INTO \"__EFMigrationsHistory\" VALUES ('20250828094150_InitialCreate', '7.0.0') ON CONFLICT DO NOTHING");
            }
        }

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        try
        {
            _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                TablesToIgnore = ["__EFMigrationsHistory"],
                DbAdapter = DbAdapter.Postgres
            });
        }
        finally
        {
            await connection.CloseAsync();
        }

        Console.WriteLine(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task ResetDatabaseAsync()
    {
        if (_respawner is null) throw new InvalidOperationException("Respawner not initialized");

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        try
        {
            await _respawner.ResetAsync(connection);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task SeedTestData(IdentityDbContext context)
    {
        var identityFaker = new Faker<Domain.Identity>()
            .RuleFor(i => i.Provider, (f) => f.PickRandom<AuthProvider>().ToDbValue());

        var identities = identityFaker.Generate(10);
        await context.Identities.AddRangeAsync(identities);
        await context.SaveChangesAsync();
    }
    
    public IdentityDbContext GetIdentityDbContextAsync()
    {
        return new IdentityDesignTimeContextFactory().CreateDbContext([_connectionString!]);
    }
}