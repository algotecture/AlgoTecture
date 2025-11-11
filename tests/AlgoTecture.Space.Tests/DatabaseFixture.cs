using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlgoTecture.Space.Infrastructure;
using AlgoTecture.Space.Infrastructure.Persistence;
using AutoBogus;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Geometries;
using Npgsql;
using Respawn;
using Xunit;

namespace AlgoTecture.Space.Tests;

public class DatabaseFixture : IAsyncLifetime
{
    private Respawner? _respawner;
    public IConfiguration Configuration { get; }

    private readonly string? _connectionString;

    public int SpaceCount = 0;

    public DatabaseFixture()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        _connectionString = Configuration.GetConnectionString("AlgoTecturePostgresSpace");
        if (string.IsNullOrEmpty(_connectionString)) throw new InvalidOperationException("Connection string is null");
    }

    public async Task InitializeAsync()
    {
        var context = new SpaceDesignTimeContextFactory().CreateDbContext([_connectionString!]);

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
                    "INSERT INTO \"__EFMigrationsHistory\" VALUES ('20250910130001_Initial', '7.0.0') ON CONFLICT DO NOTHING");
            }
        }

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        try
        {
            _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                TablesToIgnore = ["__EFMigrationsHistory", "SpaceTypes", "spatial_ref_sys"],
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

    public async Task SeedTestData(SpaceDbContext context)
    {
        var spaceFaker = new Faker<Domain.Space>()
            .RuleFor(i => i.SpaceTypeId, faker => faker.PickRandom(1,1));
        var spaces = spaceFaker.Generate(30);
        var count = 0;
        foreach (var space in spaces)
        {
            double lat = 47.3741373184 + count;
            double lon = 8.5120681827 + count;
           space.Location = new Point(lat, lon);; 
           count++;
        }
        SpaceCount = spaces.Count();
        await context.Spaces.AddRangeAsync(spaces);
        await context.SaveChangesAsync();
    }
    
    public SpaceDbContext GetSpaceDbContextAsync()
    {
        return new SpaceDesignTimeContextFactory().CreateDbContext([_connectionString!]);
    }
}