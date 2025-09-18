using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.User.Infrastructure.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {}
    public DbSet<AlgoTecture.User.Domain.User> Users => Set<AlgoTecture.User.Domain.User>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        var e = b.Entity<AlgoTecture.User.Domain.User>();
        e.ToTable("Users");
        e.HasKey(x => x.Id);
        e.Property(x => x.Email);
        e.Property(x => x.FullName);
        e.Property(x => x.Phone);
        e.Property(x=>x.CarNumbers).HasColumnType("jsonb");
        e.HasIndex(u => u.CarNumbers).HasMethod("gin");
    }
}