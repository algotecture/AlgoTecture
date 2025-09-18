namespace AlgoTecture.Identity.Domain;

public class Identity
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public required string Provider { get; set; }

    public string? ProviderUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //ToDo Deleted, Banned
}