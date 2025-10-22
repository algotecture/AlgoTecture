using AlgoTecture.Identity.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AlgoTecture.Identity.Tests.Fixtures;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public TestWebApplicationFactory(string connectionString) => _connectionString = connectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<IdentityDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<IdentityDbContext>(o => o.UseNpgsql(_connectionString));

            services.AddMassTransitTestHarness(cfg =>
            {
                cfg.AddConsumers(typeof(Program).Assembly);
            });
        });
    }
}