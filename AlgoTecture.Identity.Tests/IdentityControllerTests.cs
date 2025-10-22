using System.Net;
using System.Net.Http.Json;
using AlgoTecture.Identity.Contracts.Commands;
using AlgoTecture.Identity.Tests.Fixtures;
using Xunit;

namespace AlgoTecture.Identity.Tests;

[Collection("Identity Database collection")]
public class IdentityControllerTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _db;
    private readonly TestWebApplicationFactory _factory;

    public IdentityControllerTests(DatabaseFixture db)
    {
        _db = db;
        _factory = new TestWebApplicationFactory(_db.ConnectionString);
    }

    [Fact]
    public async Task TelegramLogin_Should_ReturnOk()
    {
        await _db.ResetDatabaseAsync();

        var client = _factory.CreateClient();
        
        var context = _db.GetIdentityDbContext();
        
        await _db.SeedTestData(context);

        var command = new TelegramLoginCommand(123456, "Test User");

        var response = await client.PostAsJsonAsync("/api/auth/telegram-login", command);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<TelegramLoginResult>();
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.IdentityId);
    }
}