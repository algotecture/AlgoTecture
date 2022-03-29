namespace AlgoTecture.Domain.Models.RepositoryModels
{
    public class User
    {
        public long Id { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public long? TelegramUserInfoId { get; set; }

        public TelegramUserInfo? TelegramUserInfo { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}