using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Identity.Infrastructure1.Persistence;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) {}
    public DbSet<Algotecture.Identity.Domain.Identity> Identities => Set<Algotecture.Identity.Domain.Identity>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        var e = b.Entity<Algotecture.Identity.Domain.Identity>();
        e.ToTable("Identities");
        e.HasKey(x => x.Id);
        e.Property(x => x.UserId);
        e.Property(x => x.Provider).IsRequired();
        e.Property(x => x.ExternalId);
    }
}