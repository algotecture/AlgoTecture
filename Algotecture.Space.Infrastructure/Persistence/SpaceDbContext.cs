using Algotecture.Space.Domain;
using Microsoft.EntityFrameworkCore;

namespace Algotecture.Space.Infrastructure.Persistence;

public class SpaceDbContext : DbContext
{
    public SpaceDbContext(DbContextOptions<SpaceDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Space> Spaces => Set<Domain.Space>();

    public DbSet<SpaceType> SpaceTypes => Set<Domain.SpaceType>();

    public DbSet<SpaceImage> SpaceImages => Set<Domain.SpaceImage>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Domain.Space>(entity =>
        {
            entity.ToTable("Spaces");
            entity.HasKey(e => e.Id);

            entity.HasOne(s => s.Parent)
                .WithMany(s => s.Children)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(s => s.ParentId);
            entity.HasIndex(s => s.SpaceTypeId);
            entity.HasIndex(s => new { s.Latitude, s.Longitude });

            entity.Property(s => s.Name)
                .HasMaxLength(255)
                .IsRequired(false);

            entity.Property(s => s.Description)
                .HasMaxLength(2000)
                .IsRequired(false);

            entity.Property(s => s.SpaceAddress)
                .HasMaxLength(500)
                .IsRequired(false);

            entity.Property(s => s.SpaceProperties)
                .IsRequired(false);

            entity.Property(s => s.Area);

            entity.Property(s => s.Latitude);

            entity.Property(s => s.Longitude);

            entity.Property(s => s.CreatedAt);
        });

        b.Entity<SpaceType>(entity =>
        {
            entity.ToTable("SpaceTypes");
            entity.HasKey(x => new { x.Id });
            entity.Property(x => x.Name).HasMaxLength(500).IsRequired();

            entity.HasData(
                new SpaceType { Id = 1, Name = "Parking" },
                new SpaceType { Id = 2, Name = "Coworking" },
                new SpaceType { Id = 3, Name = "Boat" }
            );
        });

        b.Entity<SpaceImage>(entity =>
        {
            entity.ToTable("SpaceImages");
            entity.HasKey(si => si.Id);

            entity.HasOne(si => si.Space)
                .WithMany(s => s.Images)
                .HasForeignKey(si => si.SpaceId);

            entity.Property(si => si.Url)
                .HasMaxLength(2000);

            entity.Property(si => si.Path)
                .HasMaxLength(2000);


            entity.Property(si => si.ContentType)
                .HasMaxLength(100);

            entity.Property(si => si.CreatedAt);

            entity.HasIndex(si => si.SpaceId);
            entity.HasIndex(si => si.CreatedAt);
        });
    }
}