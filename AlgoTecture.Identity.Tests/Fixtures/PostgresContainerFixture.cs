using Testcontainers.PostgreSql;
using Xunit;

namespace AlgoTecture.Identity.Tests.Fixtures;

public class PostgresContainerFixture : IAsyncLifetime
{
    public PostgreSqlContainer Container { get; private set; } = default!;
    public string ConnectionString => Container.GetConnectionString();

    public async Task InitializeAsync()
    {
        Container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("identity_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        await Container.StartAsync();
        
        Console.WriteLine($"🚀 Postgres started:");
        Console.WriteLine($"   Host: {Container.Hostname}");
        Console.WriteLine($"   Port: {Container.GetMappedPublicPort(5432)}");
        Console.WriteLine($"   Connection string: {Container.GetConnectionString()}");
    }

    public async Task DisposeAsync() => await Container.DisposeAsync(); 
}