using System;
using AlgoTecMvc.Models.Entities;
using AlgoTecMvc.Models.RepositoryModels;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<TypeOfSpace> TypeOfSpaces { get; set; }

        public virtual DbSet<Space> Spaces { get; set; }
        
        public virtual DbSet<Contract> Contracts { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        } 
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureUsersModelCreation(modelBuilder);
            ConfigureSpacesModelCreation(modelBuilder);
            ConfigureTypeOfSpacesModelCreation(modelBuilder);
            ConfigureContractsModelCreation(modelBuilder);
        }
        
        private static void ConfigureUsersModelCreation(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            modelBuilder.Entity<User>().HasKey(x => new { x.Id});
            modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(500);
            modelBuilder.Entity<User>().Property(x => x.Surname).HasMaxLength(500);
            modelBuilder.Entity<User>().Property(x => x.Patronymic).HasMaxLength(500);
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
    }
}