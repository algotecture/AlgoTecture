namespace AlgoTecture.User.Domain;

public class User
{
    public Guid Id { get; set; }
    
    public string? FullName { get; set; }
    
    public string? Email { get; set; }
    
    public string? Phone { get; set; }
    

    public string CarNumbers { get; set; } = "{}";
    
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    

    public bool IsDeleted { get; set; }
}