using AlgoTecture.Reservation.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Reservation.Infrastructure.Persistence;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options) {}
    public DbSet<Domain.Reservation> Reservations => Set<AlgoTecture.Reservation.Domain.Reservation>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Domain.Reservation>(builder =>
        {
            builder.ToTable("Reservations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SpaceId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.StartUtc)
                .IsRequired();

            builder.Property(x => x.EndUtc)
                .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();

            builder.Property(x => x.ConfirmedAtUtc);
            builder.Property(x => x.CancelledAtUtc);
            builder.Property(x => x.CompletedAtUtc);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(ReservationStatus.Pending)
                .IsRequired();

            builder.Property(x => x.PublicId)
                .HasMaxLength(50);

            builder.Property(x => x.TotalPrice)
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.Currency)
                .HasMaxLength(5);

            builder.Property(x => x.Metadata)
                .HasColumnType("jsonb")
                .HasDefaultValueSql("'{}'::jsonb")
                .IsRequired();

            builder.HasIndex(x => new { x.SpaceId, x.StartUtc, x.EndUtc });
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.Status);
        });
    }
}