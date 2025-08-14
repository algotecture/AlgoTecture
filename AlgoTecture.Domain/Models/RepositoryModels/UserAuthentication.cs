namespace AlgoTecture.Domain.Models.RepositoryModels
{
    public class UserAuthentication
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public User? User { get; set; }

        public string HashedPassword { get; set; } = null!;
    }
}