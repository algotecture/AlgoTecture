using AlgoTecture.TelegramBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.TelegramBot.Infrastructure.Persistence;

public class TelegramAccountDbContext : DbContext
{
    public TelegramAccountDbContext(DbContextOptions<TelegramAccountDbContext> options) : base(options) {}
    
    public DbSet<TelegramAccount> TelegramAccounts => Set<TelegramAccount>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        var e = b.Entity<TelegramAccount>();
        e.ToTable("TelegramAccounts");
        e.HasKey(e => e.Id);
        e.HasIndex(e => e.TelegramUserId).IsUnique();
        e.HasIndex(e => e.LinkedUserId);
        e.Property(e => e.TelegramUserId)
            .IsRequired();
        e.Property(e => e.TelegramUserName)
            .HasMaxLength(64);
        e.Property(e => e.TelegramUserFullName)
            .IsRequired()
            .HasMaxLength(100);
        e.Property(e => e.LanguageCode)
            .HasMaxLength(10);
        e.Property(e => e.CreatedAt)
            .IsRequired();
        e.Property(e => e.LastActivityAt)
            .IsRequired();
        e.Property(e => e.IsBlocked)
            .HasDefaultValue(false)
            .IsRequired();
    }
}