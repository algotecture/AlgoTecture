using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AlgoTecture.Space.Contracts.Dto;
using Xunit;

namespace AlgoTecture.Space.Tests.Integration;

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
        var spaces = await client.GetFromJsonAsync<List<SpaceDto>>("http://localhost:5000/api/space/nearest-by-type/47.3741373184/8.5120681827/1/10000000/10");
        
        await _databaseFixture.DisposeAsync();
        // Assert
        Assert.Equal(spaces.First().GetType(), typeof(SpaceDto));
    }   
}