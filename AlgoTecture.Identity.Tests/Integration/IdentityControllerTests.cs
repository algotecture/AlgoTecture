using System.Net;
using System.Text;
using AlgoTecture.Identity.Contracts.Commands;
using Newtonsoft.Json;
using Xunit;

namespace AlgoTecture.Identity.Tests.Integration;

public class IdentityControllerTests : IClassFixture<DatabaseFixture>
{
    
    private readonly DatabaseFixture _databaseFixture;

    public IdentityControllerTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public async Task TelegramLogin_ReturnsHttpStatusCodeOK()
    {
        await _databaseFixture.ResetDatabaseAsync();
        await _databaseFixture.SeedTestData(_databaseFixture.GetIdentityDbContextAsync());
        // Arrange
        var data = new TelegramLoginCommand(123, "sss");
        var jsonContent = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        // Act
        using var client = new HttpClient(){
        };
        var response = await client.PostAsync("http://localhost:5000/api/auth/telegram-login", content);
        
        await _databaseFixture.DisposeAsync();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }   
}