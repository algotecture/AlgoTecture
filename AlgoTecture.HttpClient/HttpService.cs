using System.Net.Http.Json;

namespace AlgoTecture.HttpClient;

public interface IHttpService
{
    Task<string> GetAsync(string url, CancellationToken cancellationToken = default);

    Task<string> PostAsync<T>(string url, T content, CancellationToken cancellationToken = default);
}

public class HttpService : IHttpService
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public HttpService(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAsync(string url, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            throw new HttpServiceException($"Request to {url} failed", ex);
        }
    }
    
    public async Task<string> PostAsync<T>(string url, T content, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, content, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            throw new HttpServiceException($"POST {url} failed", ex);
        }
    }
}

public class HttpServiceException : Exception
{
    public HttpServiceException(string message, Exception innerException) 
        : base(message, innerException) { }
}