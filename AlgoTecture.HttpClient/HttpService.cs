using System.Net.Http.Json;

namespace AlgoTecture.HttpClient;

public interface IHttpService
{
    Task<TResponse?> GetAsync<TResponse>(string url, CancellationToken cancellationToken = default);

    Task<string> PostAsync<T>(string url, T content, CancellationToken cancellationToken = default);
    
    Task<TResponse> PostAsync<TRequest, TResponse>( string url, TRequest data, CancellationToken cancellationToken = default);
}


public class HttpService : IHttpService
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public HttpService(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse?> GetAsync<TResponse>(string url, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);
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
    
    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string url,
        TRequest data,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, data, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TResponse>(
                cancellationToken: cancellationToken);

            if (result is null)
                throw new HttpServiceException($"Empty response for POST {url}");

            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new HttpServiceException($"Request to {url} failed", ex);
        }
    }
}

public class HttpServiceException : Exception
{
    public HttpServiceException(string message, Exception innerException = null) 
        : base(message, innerException) { }
}