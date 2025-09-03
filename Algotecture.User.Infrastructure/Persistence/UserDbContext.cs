using Microsoft.EntityFrameworkCore;

namespace Algotecture.User.Infrastructure.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {}
    public DbSet<Algotecture.User.Domain.User> Users => Set<Algotecture.User.Domain.User>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        var e = b.Entity<Algotecture.User.Domain.User>();
        e.ToTable("Users");
        e.HasKey(x => x.Id);
        e.Property(x => x.Email);
        e.Property(x => x.FullName);
        e.Property(x => x.Phone);
    }
}