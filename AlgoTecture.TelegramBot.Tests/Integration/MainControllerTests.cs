using AlgoTecture.HttpClient;
using AlgoTecture.TelegramBot.Api.Controllers;
using Xunit;

namespace AlgoTecture.TelegramBot.Tests.Integration;

public class MainControllerTests
{
    [Fact]
    public async Task Start_ShouldLoginUserViaIdentityService()
    {
        // Arrange
        var httpService = new HttpService(new System.Net.Http.HttpClient
        {
          //  BaseAddress = new Uri("http://localhost:5000/identity/api/auth/telegram-login")
        });

        var controller = new MainController(httpService);

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