namespace AlgoTecture.TelegramBot.Models;

public class DeepSeekConfig
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://openrouter.ai/api/v1";
    public string Model { get; set; } = "deepseek/deepseek-chat-v3-0324";
}