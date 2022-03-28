using AlgoTecture.Domain.Models.Entities;
using AlgoTecture.Domain.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlgoTecture.EfCli
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<TypeOfSpace> TypeOfSpaces { get; set; }

        public virtual DbSet<Space> Spaces { get; set; }
        
        public virtual DbSet<Contract> Contracts { get; set; }
        
        public virtual DbSet<UtilizationType> UtilizationTypes { get; set; }
        
        public virtual DbSet<UserAuthentication> UserAuthentications { get; set; }

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
            ConfigureTypeOfSpacesModelCreation(modelBuilder);
            ConfigureContractsModelCreation(modelBuilder);
            ConfigureUtilizationTypesModelCreation(modelBuilder);
            ConfigureUserAuthenticationsModelCreation(modelBuilder);
            
            modelBuilder.Entity<TypeOfSpace>().HasData(new TypeOfSpace{ Id = 1, Name = "Public buildings and structures"});
            modelBuilder.Entity<TypeOfSpace>().HasData(new TypeOfSpace{ Id = 2, Name = "Residential buildings"});
            modelBuilder.Entity<TypeOfSpace>().HasData(new TypeOfSpace{ Id = 3, Name = "Industrial buildings and structures"});
            modelBuilder.Entity<TypeOfSpace>().HasData(new TypeOfSpace{ Id = 4, Name = "Buildings and structures intended for the needs of agriculture"});
            
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 1, Name = "Residential"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 2, Name = "Сommercial"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 3, Name = "Production"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 4, Name = "Warehouse"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 5, Name = "Public catering"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 6, Name = "Utility"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 7, Name = "Office space"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 8, Name = "Education"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 9, Name = "Sports"});
            modelBuilder.Entity<UtilizationType>().HasData(new UtilizationType{ Id = 10, Name = "Free target"});
        }
        
        private static void ConfigureUsersModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<User>().HasKey(x => new { x.Id});
            modelBuilder.Entity<User>().Property(x => x.Phone).HasMaxLength(500);
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(500);
            modelBuilder.Entity<User>().HasIndex(x => x.Email);
        }
        
        private static void ConfigureSpacesModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<Space>().HasKey(x => new { x.Id});
            modelBuilder.Entity<Space>().Property(x => x.SpaceAddress).HasMaxLength(500);
            modelBuilder.Entity<Space>().HasIndex(x => x.Latitude);
            modelBuilder.Entity<Space>().HasIndex(x => x.Longitude);
        }
        
        private static void ConfigureTypeOfSpacesModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<TypeOfSpace>().HasKey(x => new { x.Id});
            modelBuilder.Entity<TypeOfSpace>().Property(x => x.Name).HasMaxLength(500);
        }
        
        private static void ConfigureContractsModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<Contract>().HasKey(x => new { x.Id});
        }
        
        private static void ConfigureUtilizationTypesModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<UtilizationType>().HasKey(x => new { x.Id});
            modelBuilder.Entity<UtilizationType>().Property(x => x.Name).HasMaxLength(500);
        }
        
        private static void ConfigureUserAuthenticationsModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<UserAuthentication>().HasKey(x => new { x.Id});
            modelBuilder.Entity<UserAuthentication>().HasIndex(x => x.UserId);
            modelBuilder.Entity<UserAuthentication>().Property(x => x.HashedPassword).HasMaxLength(500);
        }
    }
}