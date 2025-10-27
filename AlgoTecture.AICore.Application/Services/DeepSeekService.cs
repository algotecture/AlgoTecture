using System.Text;
using System.Text.Json;

namespace AlgoTecture.AICoreService.Application.Services;

public class DeepSeekService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly string _model;

    public DeepSeekService(string apiKey, string baseUrl, string model)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey.Trim();
        _baseUrl = baseUrl.TrimEnd('/');
        _model = model;
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        var request = new
        {
            model = _model,
            messages = new[]
            {
                new { role = "system", content = "You are a JSON assistant that extracts booking intent and parameters from text." },
                new { role = "user", content = prompt }
            },
            max_tokens = 1000
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("AlgoTecture-TelegramBot/1.0");

        var url = $"{_baseUrl}/chat/completions";
        var response = await _httpClient.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Error: {response.StatusCode} - {responseContent}");
        }

        var result = JsonSerializer.Deserialize<DeepSeekResponse>(responseContent);
        return result?.choices[0].message.content ?? string.Empty;
    }

    private class DeepSeekResponse
    {
        public Choice[] choices { get; set; }
    }

    private class Choice
    {
        public Message message { get; set; }
    }

    private class Message
    {
        public string content { get; set; }
    }
}
