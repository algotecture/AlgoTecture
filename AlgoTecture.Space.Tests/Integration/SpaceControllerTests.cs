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
        var spaces = await client.GetFromJsonAsync<List<GetSpacesByTypeDto>>("http://localhost:5000/api/space/by-type/1");
        
        await _databaseFixture.DisposeAsync();
        // Assert
        Assert.Equal(spaces.First().GetType(), typeof(GetSpacesByTypeDto));
    }   
}