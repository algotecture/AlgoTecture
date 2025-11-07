using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Identity.Infrastructure.Persistence;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) {}
    public DbSet<AlgoTecture.Identity.Domain.Identity> Identities => Set<AlgoTecture.Identity.Domain.Identity>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        var e = b.Entity<AlgoTecture.Identity.Domain.Identity>();
        e.ToTable("Identities");
        e.HasKey(x => x.Id);
        e.Property(x => x.UserId);
        e.Property(x => x.Provider).IsRequired();
        e.Property(x => x.ProviderUserId);
        
        b.AddOutboxMessageEntity();
        
        b.Entity<OutboxState>(entity =>
        {
            entity.HasKey(e => e.OutboxId);
        });
    }
}