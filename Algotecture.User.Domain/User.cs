namespace Algotecture.User.Domain;

public class User
{
    public long Id { get; set; }
    
    public string? FullName { get; set; }
    
    public string? Email { get; set; }
    
    public string? Phone { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; }
}