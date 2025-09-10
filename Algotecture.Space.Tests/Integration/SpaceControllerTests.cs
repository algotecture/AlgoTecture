using System.Net;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace Algotecture.Space.Tests.Integration;

public class SpaceControllerTests : IClassFixture<DatabaseFixture>
{
    
    private readonly DatabaseFixture _databaseFixture;

    public SpaceControllerTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public async Task GetByType_ReturnsListSpaces()
    {
        await _databaseFixture.ResetDatabaseAsync();
        await _databaseFixture.SeedTestData(_databaseFixture.GetSpaceDbContextAsync());

        // Act
        using var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5010/api/space/by-type/1");
        
        await _databaseFixture.DisposeAsync();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }   
}