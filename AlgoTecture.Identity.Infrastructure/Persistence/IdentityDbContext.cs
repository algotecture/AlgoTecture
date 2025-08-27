using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Identity.Infrastructure.Persistence;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) {}
    public DbSet<Domain.Identity> Identities => Set<Domain.Identity>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        var e = b.Entity<Domain.Identity>();
        e.ToTable("Identities");
        e.HasKey(x => x.Id);
        e.Property(x => x.UserId);
        e.Property(x => x.Provider).IsRequired();
        e.Property(x => x.ExternalId);
    }
}