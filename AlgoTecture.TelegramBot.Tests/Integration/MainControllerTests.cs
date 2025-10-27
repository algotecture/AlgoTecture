using AlgoTecture.GeoAdminSearch;
using AlgoTecture.HttpClient;
using AlgoTecture.Identity.Contracts;
using AlgoTecture.TelegramBot.Api.Controllers;
using AlgoTecture.TelegramBot.Application;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Infrastructure;
using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Xunit;

namespace AlgoTecture.TelegramBot.Tests.Integration;

public class MainControllerTests
{
    [Fact]
    public async Task Start_ShouldLoginUserViaIdentityService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Redis
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = "localhost:6379"; 
            opt.InstanceName = "TelegramBot_";
        });
        services.AddScoped<IUserCache, UserCache>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.Test.json", optional: false) 
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresTelegramAccountTest");
        
        services.AddDbContext<TelegramAccountDbContext>(opt =>
            opt.UseNpgsql(connectionString));
        services.AddScoped<ITelegramAccountDbContext>(sp => 
            sp.GetRequiredService<TelegramAccountDbContext>());

        services.AddHttpClient<HttpService>();
        services.AddScoped<ITelegramBotService, TelegramBotService>();
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<IReservationFlowService, ReservationFlowService>();
        services.AddScoped<IGeoAdminSearcher, GeoAdminSearcher>();
        services.AddScoped<IHttpService, HttpService>();
        
        services.AddRefitClient<IAuthApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000/identity"));
        
        services.AddScoped<MainController>();

        var provider = services.BuildServiceProvider();
        var controller = provider.GetRequiredService<MainController>();
        
        
        controller.SetFakeContext(
            chatId: 12345,
            userId: 781,
            username: "integration_test",
            fullName: "Integration Test User"
        );

        // Act
        await controller.Start();
    }
}