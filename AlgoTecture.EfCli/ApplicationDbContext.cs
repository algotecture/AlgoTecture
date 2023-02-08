using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.Entities;
using AlgoTecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AlgoTecture.EfCli
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Space> Spaces { get; set; }

        public virtual DbSet<Contract> Contracts { get; set; }

        public virtual DbSet<UtilizationType> UtilizationTypes { get; set; }

        public virtual DbSet<UserAuthentication> UserAuthentications { get; set; }

        public virtual DbSet<TelegramUserInfo> TelegramUserInfos { get; set; }

        public ApplicationDbContext()
        {
            //  Database.EnsureCreated();
        }

        public ApplicationDbContext(int timeOut)
        {
            Database.SetCommandTimeout(timeOut);
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            //  Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appConnectionString = Configurator.GetConfiguration().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(appConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureUsersModelCreation(modelBuilder);
            ConfigureSpacesModelCreation(modelBuilder);
            ConfigureContractsModelCreation(modelBuilder);
            ConfigureUtilizationTypesModelCreation(modelBuilder);
            ConfigureUserAuthenticationsModelCreation(modelBuilder);
            ConfigureTelegramUserInfosModelCreation(modelBuilder);

            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 1, Name = "Residential" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 2, Name = "Сommercial" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 3, Name = "Production" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 4, Name = "Warehouse" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 5, Name = "Public catering" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 6, Name = "Utility" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 7, Name = "Office space" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 8, Name = "Education" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 9, Name = "Sports" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 10, Name = "Free target" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 11, Name = "Parking" });
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 12, Name = "Boat" });

            var newSpaceId1 = Guid.NewGuid();
            var newSpaceProperty1 = new SpaceProperty
            {
                SpaceId = 1,
                Name = "Pedro boat",
                SpacePropertyId = Guid.NewGuid(),
                Description = "Description"
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 1, Latitude = 38.705022, Longitude = -9.145460, SpaceAddress = "Lisbon, Lisboa-Cacilhas",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty1), UtilizationTypeId = 12
            });
            var newSpaceId2 = Guid.NewGuid();
            var newSpaceProperty2 = new SpaceProperty
            {
                SpaceId = 2,
                Name = "Bartolomeu boat",
                SpacePropertyId = Guid.NewGuid(),
                Description = "Description"
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 2, Latitude = 38.705022, Longitude = -9.145460, SpaceAddress = "Lisbon, Lisboa-Cacilhas",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty2), UtilizationTypeId = 12
            });
            var newSpaceId3 = Guid.NewGuid();
            var newSpaceProperty3 = new SpaceProperty
            {
                SpaceId = 3,
                Name = "Vashka boat",
                SpacePropertyId = Guid.NewGuid(),
                Description = "Description"
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 3, Latitude = 38.705022, Longitude = -9.145460, SpaceAddress = "Lisbon, Lisboa-Cacilhas",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty3), UtilizationTypeId = 12
            });
        }

        private static void ConfigureUsersModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<User>().HasKey(x => new { x.Id });
            modelBuilder.Entity<User>().Property(x => x.Phone).HasMaxLength(500);
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(500);
            modelBuilder.Entity<User>().HasIndex(x => x.Email);
            modelBuilder.Entity<User>().HasIndex(x => x.TelegramUserInfoId);
        }

        private static void ConfigureSpacesModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<Space>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Space>().Property(x => x.SpaceAddress).HasMaxLength(500);
            modelBuilder.Entity<Space>().HasIndex(x => x.Latitude);
            modelBuilder.Entity<Space>().HasIndex(x => x.Longitude);
        }

        private static void ConfigureContractsModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<Contract>().HasKey(x => new { x.Id });
        }

        private static void ConfigureUtilizationTypesModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<UtilizationType>().HasKey(x => new { x.Id });
            modelBuilder.Entity<UtilizationType>().Property(x => x.Name).HasMaxLength(500);
        }

        private static void ConfigureUserAuthenticationsModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<UserAuthentication>().HasKey(x => new { x.Id });
            modelBuilder.Entity<UserAuthentication>().HasIndex(x => x.UserId);
            modelBuilder.Entity<UserAuthentication>().Property(x => x.HashedPassword).HasMaxLength(500);
        }

        private static void ConfigureTelegramUserInfosModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<TelegramUserInfo>().HasKey(x => new { x.Id });
            modelBuilder.Entity<TelegramUserInfo>().Property(x => x.TelegramUserName).HasMaxLength(500);
            modelBuilder.Entity<TelegramUserInfo>().Property(x => x.TelegramUserFullName).HasMaxLength(500);
        }
    }
}