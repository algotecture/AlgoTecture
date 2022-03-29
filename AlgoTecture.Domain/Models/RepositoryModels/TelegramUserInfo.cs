namespace AlgoTecture.Domain.Models.RepositoryModels;

public class TelegramUserInfo
{
    public long Id { get; set; }

    public long? TelegramUserId { get; set; }

    public long? TelegramChatId { get; set; }

    public string? TelegramUserName { get; set; }

    public string? TelegramUserFullName { get; set; }
}