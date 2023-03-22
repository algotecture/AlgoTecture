using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AlgoTecture.Data.Persistence.Ef
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString = string.Empty;

        private readonly bool _isTest = false;

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Space> Spaces { get; set; }

        public virtual DbSet<Contract> Contracts { get; set; }

        public virtual DbSet<UtilizationType> UtilizationTypes { get; set; }

        public virtual DbSet<UserAuthentication> UserAuthentications { get; set; }

        public virtual DbSet<TelegramUserInfo> TelegramUserInfos { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<PriceSpecification> PriceSpecifications { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(string connectionString, bool isTest)
        {
            _connectionString = connectionString;
            _isTest = isTest;
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_isTest)
            {
                optionsBuilder.UseInMemoryDatabase("AppInMemory");
            }
            else if (!string.IsNullOrEmpty(_connectionString) && !_isTest)
            {
                optionsBuilder.UseSqlite(_connectionString);
            }
            else
            {
                var appConnectionString = string.Empty;

                if (OperatingSystem.IsLinux())
                {
                    appConnectionString = Configurator.GetConfiguration().GetConnectionString("DemoConnection");
                }

                if (OperatingSystem.IsWindows())
                {
                    appConnectionString = Configurator.GetConfiguration().GetConnectionString("WindowsSqlLiteDevelopingConnection");
                }

                if (OperatingSystem.IsMacOS())
                {
                    appConnectionString = Configurator.GetConfiguration().GetConnectionString("DefaultConnection");
                }

                optionsBuilder.UseSqlite(appConnectionString);
            }
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

            modelBuilder.Entity<User>().HasData(new User { Id = 1, CreateDateTime = new DateTime(2023, 02, 21) });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, CreateDateTime = new DateTime(2023, 03, 14) });
            modelBuilder.Entity<User>().HasData(new User { Id = 3, CreateDateTime = new DateTime(2023, 03, 14) });
            modelBuilder.Entity<User>().HasData(new User { Id = 4, CreateDateTime = new DateTime(2023, 03, 16) });


            var newSpaceProperty1 = new SpaceProperty
            {
                SpaceId = 1,
                Name = "Santa María",
                SpacePropertyId = Guid.Parse("4c4f455c-bc98-47da-9f4b-9dcc25a17fe5"),
                Description = "Description"
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 1, Latitude = 38.705022, Longitude = -9.145460, SpaceAddress = "Lisbon, Lisboa-Cacilhas",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty1), UtilizationTypeId = 12
            });
            var newSpaceProperty2 = new SpaceProperty
            {
                SpaceId = 2,
                Name = "Niña",
                SpacePropertyId = Guid.Parse("7d2dc2f3-4f52-4244-8ade-73eba2772a51"),
                Description = "Description"
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 2, Latitude = 38.705022, Longitude = -9.145460, SpaceAddress = "Lisbon, Lisboa-Cacilhas",
                SpaceProperty = JsonConvert.SerializeObject(newSpaceProperty2), UtilizationTypeId = 12
            });
            var newSpaceProperty3 = new SpaceProperty
            {
                SpaceId = 3,
                Name = "Pinta",
                SpacePropertyId = Guid.Parse("a5f8e388-0c2f-491c-82ff-d4c92da97aaa"),
                Description = "Description"
            };
            modelBuilder.Entity<Space>().HasData(new Space
            {
                Id = 3, Latitude = 38.705022, Longitude = -9.145460, SpaceAddress = "Lisbon, Lisboa-Cacilhas",
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
                Id = 1, TenantUserId = 2, SpaceId = 1, TotalPrice = "100", PriceSpecificationId = 1, ReservationDateTime = DateTime.Parse("2023-03-16 15:00"),
                ReservationFrom = DateTime.Parse("2023-03-17 15:00"), ReservationTo = DateTime.Parse("2023-03-17 17:00"),
                ReservationStatus = "Confirmed"
            });
            
            modelBuilder.Entity<Reservation>().HasData(new Reservation
            {
                Id = 2, TenantUserId = 3, SpaceId = 1, TotalPrice = "100", PriceSpecificationId = 1, ReservationDateTime = DateTime.Parse("2023-03-17 15:00"),
                ReservationFrom = DateTime.Parse("2023-03-18 15:00"), ReservationTo = DateTime.Parse("2023-03-18 18:00"),
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
            modelBuilder.Entity<Contract>().HasIndex(x => x.ReservationId);
            modelBuilder.Entity<Contract>().HasIndex(x => x.SpaceId);
            modelBuilder.Entity<Contract>().HasIndex(x => x.SubSpaceId);
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