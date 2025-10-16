using AlgoTecture.HttpClient;
using AlgoTecture.Space.Contracts.Dto;

namespace AlgoTecture.TelegramBot.Application.Services;

public interface ISpaceServiceClient
{
    Task<List<SpaceDto>> GetByTypeAsync(int spaceTypeId, CancellationToken ct = default);
    
    Task<List<SpaceDto>> GetNearestByTypeAsync(
        double latitude, 
        double longitude, 
        int spaceTypeId,
        int maxDistanceMeters,
        int count,
        CancellationToken ct = default);
}

public class SpaceServiceClient : ISpaceServiceClient
{
    private readonly IHttpService _httpService;

    public SpaceServiceClient(IHttpService httpService)
    {
        _httpService = httpService;
    }
    
    public async Task<List<SpaceDto>> GetByTypeAsync(int spaceTypeId, CancellationToken ct = default)
    {
        var result = await _httpService.GetAsync<List<SpaceDto>>($"http://localhost:5000/space/api/space/by-type/{spaceTypeId}", ct);

        return result ?? [];
    }

    public async Task<List<SpaceDto>> GetNearestByTypeAsync(
        double latitude, double longitude, int spaceTypeId, int maxDistanceMeters, int count, CancellationToken ct = default)
    {
        var result = await _httpService.GetAsync<List<SpaceDto>>(
            $"http://localhost:5000/space/api/space/nearest-by-type/{latitude}/{longitude}/{spaceTypeId}/{maxDistanceMeters}/{count}",
            ct);

        return result ?? [];
    }
}