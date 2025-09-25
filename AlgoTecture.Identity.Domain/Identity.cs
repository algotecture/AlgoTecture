namespace AlgoTecture.Identity.Domain;

public class Identity
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public required string Provider { get; set; }

    public string? ProviderUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //ToDo Deleted, Banned
}