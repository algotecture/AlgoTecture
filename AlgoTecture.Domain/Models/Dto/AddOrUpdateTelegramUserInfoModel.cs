namespace AlgoTecture.Domain.Models.Dto;

public class AddOrUpdateTelegramUserInfoModel
{
    public long? TelegramUserId { get; set; }

    public long? TelegramChatId { get; set; }

    public string? TelegramUserName { get; set; }

    public string? TelegramUserFullName { get; set; }
}