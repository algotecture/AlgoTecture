using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Environments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlgoTecture.Data.Persistence.Ef
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<Space> Spaces { get; set; } = null!;

        public virtual DbSet<UtilizationType> UtilizationTypes { get; set; } = null!;

        public virtual DbSet<UserAuthentication> UserAuthentications { get; set; } = null!;

        public virtual DbSet<TelegramUserInfo> TelegramUserInfos { get; set; } = null!;

        public virtual DbSet<Reservation> Reservations { get; set; } = null!;

        public virtual DbSet<PriceSpecification> PriceSpecifications { get; set; } = null!;

        private readonly Provider _provider = Provider.NpgSql;

        public ApplicationDbContext() { }

        public ApplicationDbContext(Provider provider = Provider.NpgSql)
        {
            _provider = provider;
        }
        
        private static readonly ILoggerFactory Logger = LoggerFactory.Create(builder => { builder.AddConsole(); });
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_provider == Provider.InMemory)
            {
                optionsBuilder.UseLoggerFactory(Logger).UseInMemoryDatabase("Data Source=:memory:");
                return;
            }

            var appConnectionString = Configurator.GetConfiguration().GetConnectionString("Algotecture-Demo");

            if (string.IsNullOrEmpty(appConnectionString)) return;

            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(appConnectionString);
            }
            else
            {
                optionsBuilder.UseLoggerFactory(Logger).UseNpgsql(appConnectionString);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureUsersModelCreation(modelBuilder);
            ConfigureSpacesModelCreation(modelBuilder);
            ConfigureUtilizationTypesModelCreation(modelBuilder);
            ConfigureUserAuthenticationsModelCreation(modelBuilder);
            ConfigureTelegramUserInfosModelCreation(modelBuilder);
            ConfigureReservationsModelCreation(modelBuilder);
            ConfigurePriceSpecificationModelCreation(modelBuilder);
            
            //Data seeding to tests

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
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType { Id = 13, Name = "Coworking" });

            modelBuilder.Entity<User>().HasData(new User { Id = 1, CreateDateTimeUtc = new DateTime(2023, 02, 21).ToUniversalTime() });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, CreateDateTimeUtc = new DateTime(2023, 03, 14).ToUniversalTime() });
            modelBuilder.Entity<User>().HasData(new User { Id = 3, CreateDateTimeUtc = new DateTime(2023, 03, 14).ToUniversalTime() });
            modelBuilder.Entity<User>().HasData(new User { Id = 4, CreateDateTimeUtc = new DateTime(2023, 03, 16).ToUniversalTime() });
            
            
            var newSpaceProperty1 = new SpaceProperty
            {
                Name = "Santa María",
                SpacePropertyId = Guid.Parse("4c4f455c-bc98-47da-9f4b-9dcc25a17fe5"),
                Description = "best boat in the world",
                Properties = new Dictionary<string, string>(){{"additionalProp1", "string"}, {"additionalProp2", "string"}, {"additionalProp3", "string"}},
                Images = new List<string>(){"4c4f455c-bc98-47da-9f4b-9dcc25a17fe5.jpeg"}
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 1, Latitude = 47.361812591552734, Longitude = 8.5370702743530273, SpaceAddress = "Mythenquai 7, 8002 Zürich",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty1), UtilizationTypeId = 12
            });
            var newSpaceProperty2 = new SpaceProperty
            {
                Name = "Niña",
                SpacePropertyId = Guid.Parse("7d2dc2f3-4f52-4244-8ade-73eba2772a51"),
                Description = "best boat in the world",
                Properties = new Dictionary<string, string>(){{"additionalProp1", "string"}, {"additionalProp2", "string"}, {"additionalProp3", "string"}},
                Images = new List<string>(){"7d2dc2f3-4f52-4244-8ade-73eba2772a51.jpeg"}
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 2, Latitude = 47.36164855957031, Longitude = 8.5366735458374023, SpaceAddress = "Mythenquai 9, 8002 Zürich",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty2), UtilizationTypeId = 12
            });
            var newSpaceProperty3 = new SpaceProperty
            {
                Name = "Pinta",
                SpacePropertyId = Guid.Parse("a5f8e388-0c2f-491c-82ff-d4c92da97aaa"),
                Description = "best boat in the world",
                Properties = new Dictionary<string, string>(){{"additionalProp1", "string"}, {"additionalProp2", "string"}, {"additionalProp3", "string"}},
                Images = new List<string>(){"a5f8e388-0c2f-491c-82ff-d4c92da97aaa.jpeg"}
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 3, Latitude = 47.3613166809082, Longitude = 8.5362958908081055, SpaceAddress = "Mythenquai 25, 8002 Zürich",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty3), UtilizationTypeId = 12
            });
            
            modelBuilder.Entity<PriceSpecification>().HasData(new PriceSpecification
            {
                Id = 1, SpaceId = 1, PriceCurrency = "Usd", UnitOfTime = "Hour", PricePerTime = "50"
            });
            modelBuilder.Entity<PriceSpecification>().HasData(new PriceSpecification
            {
                Id = 2, SpaceId = 2, PriceCurrency = "Usd", UnitOfTime = "Hour", PricePerTime = "45"
            });
            modelBuilder.Entity<PriceSpecification>().HasData(new PriceSpecification
            {
                Id = 3, SpaceId = 3, PriceCurrency = "Usd", UnitOfTime = "Hour", PricePerTime = "60"
            });
            
            modelBuilder.Entity<Reservation>().HasData(new Reservation
            {
                Id = 1, TenantUserId = 2, SpaceId = 1, TotalPrice = "100", PriceSpecificationId = 1, ReservationDateTimeUtc = DateTime.Parse("2023-03-16 15:00").ToUniversalTime(),
                ReservationFromUtc = DateTime.Parse("2023-03-17 15:00").ToUniversalTime(), ReservationToUtc = DateTime.Parse("2023-03-17 17:00").ToUniversalTime(),
                ReservationStatus = "Confirmed"
            });
            
            modelBuilder.Entity<Reservation>().HasData(new Reservation
            {
                Id = 2, TenantUserId = 3, SpaceId = 1, TotalPrice = "100", PriceSpecificationId = 1, ReservationDateTimeUtc = DateTime.Parse("2023-03-17 15:00").ToUniversalTime(),
                ReservationFromUtc = DateTime.Parse("2023-03-18 15:00").ToUniversalTime(), ReservationToUtc = DateTime.Parse("2023-03-18 18:00").ToUniversalTime(),
                ReservationStatus = "Confirmed"
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
            modelBuilder.Entity<User>().Property(x => x.CarNumbers).HasMaxLength(500);
        }

        private static void ConfigureSpacesModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<Space>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Space>().Property(x => x.SpaceAddress).HasMaxLength(500);
            modelBuilder.Entity<Space>().HasIndex(x => x.Latitude);
            modelBuilder.Entity<Space>().HasIndex(x => x.Longitude);
        }

        private static void ConfigureUtilizationTypesModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<UtilizationType>().HasKey(x => new { x.Id });
            modelBuilder.Entity<UtilizationType>().Property(x => x.Name).HasMaxLength(500);
        }

        private static void ConfigureReservationsModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<Reservation>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Reservation>().HasIndex(x => x.TenantUserId);
            modelBuilder.Entity<Reservation>().HasIndex(x => x.SpaceId);
            modelBuilder.Entity<Reservation>().Property(x => x.SubSpaceId).HasMaxLength(100);
            modelBuilder.Entity<Reservation>().Property(x => x.TotalPrice).HasMaxLength(100);
            modelBuilder.Entity<Reservation>().Property(x => x.ReservationStatus).HasMaxLength(100);
            modelBuilder.Entity<Reservation>().Property(x => x.Description).HasMaxLength(500);
        }

        private static void ConfigurePriceSpecificationModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<PriceSpecification>().HasKey(x => new { x.Id });
            modelBuilder.Entity<PriceSpecification>().HasIndex(x => x.SpaceId);
            modelBuilder.Entity<PriceSpecification>().Property(x => x.SubSpaceId).HasMaxLength(100);
            modelBuilder.Entity<PriceSpecification>().Property(x => x.UnitOfTime).HasMaxLength(100);
            modelBuilder.Entity<PriceSpecification>().Property(x => x.PricePerTime).HasMaxLength(100);
            modelBuilder.Entity<PriceSpecification>().Property(x => x.PriceCurrency).HasMaxLength(100);
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