namespace AlgoTecture.TelegramBot.Domain;

public class TelegramAccount
{
    public long Id { get; set; }

    public long? TelegramUserId { get; set; }

    public long? TelegramChatId { get; set; }

    public string? TelegramUserName { get; set; }

    public string? TelegramUserFullName { get; set; }

    public long? LinkedUserId { get; set; }
    
    public string? LanguageCode { get; set; }
    
    public DateTime CreatedAt { get; set; }  
    
    public DateTime LastActivityAt { get; set; } 

    public bool IsBlocked { get; set; }
}