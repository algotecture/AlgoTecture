namespace AlgoTecture.TelegramBot.Models;

public class BookingIntent
{
    public string Action { get; set; } = string.Empty;
    public Dictionary<string, string> Parameters { get; set; } = new();
    public string OriginalMessage { get; set; } = string.Empty;
    public bool IsValid { get; set; }
    
    // Вспомогательные методы для работы с параметрами
    public string GetParameter(string key, string defaultValue = "")
    {
        return Parameters.ContainsKey(key) ? Parameters[key] : defaultValue;
    }
    
    public DateTime? GetDateParameter(string key)
    {
        if (Parameters.ContainsKey(key) && DateTime.TryParse(Parameters[key], out var date))
            return date;
        return null;
    }
    
    public bool HasParameter(string key) => Parameters.ContainsKey(key);
}