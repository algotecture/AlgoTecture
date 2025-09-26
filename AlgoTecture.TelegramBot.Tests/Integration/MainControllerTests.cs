using AlgoTecture.HttpClient;
using AlgoTecture.TelegramBot.Api.Controllers;
using AlgoTecture.TelegramBot.Application;
using AlgoTecture.TelegramBot.Application.Services;
using AlgoTecture.TelegramBot.Infrastructure;
using AlgoTecture.TelegramBot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            opt.Configuration = "localhost:6379"; // реальный Redis (например, в Docker)
            opt.InstanceName = "TelegramBot_";
        });
        services.AddScoped<IUserCache, UserCache>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // путь до тестового проекта
            .AddJsonFile("appsettings.Test.json", optional: false) // отдельный конфиг для тестов
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        var connectionString = configuration.GetConnectionString("AlgoTecturePostgresTelegramAccountTest");
        // DbContext
        services.AddDbContext<TelegramAccountDbContext>(opt =>
            opt.UseNpgsql(connectionString));
        services.AddScoped<ITelegramAccountDbContext>(sp => 
            sp.GetRequiredService<TelegramAccountDbContext>());

        // Сервисы
        services.AddHttpClient<HttpService>();
        services.AddScoped<ITelegramBotService, TelegramBotService>();

        // Контроллер (тоже через DI)
        services.AddScoped<MainController>();

        var provider = services.BuildServiceProvider();
        var controller = provider.GetRequiredService<MainController>();
        
        // подставляем фейковый контекст пользователя
        controller.SetFakeContext(
            chatId: 12345,
            userId: 777,
            username: "integration_test",
            fullName: "Integration Test User"
        );

        // Act
        await controller.Start();
    }
}