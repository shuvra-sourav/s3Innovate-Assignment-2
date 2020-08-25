using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TimeseriesDatabase.Model;
namespace TimeseriesDatabase
{
    public class TimeSeriesDbContext: DbContext
    {
        public DbSet<Building> Building { get; set; }
        public DbSet<BuildingObject> BuildingObject { get; set; }
        public DbSet<BuildingDataField> BuildingDataFileld { get; set; }
        public DbSet<Reading> Reading { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Database=BuildingTimeseriesData;Data Source=.;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>().ToTable("Building");
            modelBuilder.Entity<BuildingObject>().ToTable("BuildingObject");
            modelBuilder.Entity<BuildingDataField>().ToTable("BuildingDataField");
            modelBuilder.Entity<Reading>().ToTable("Reading");
            
            modelBuilder.Entity<Building>().Property(e => e.Name).HasColumnType("VARCHAR(50)").IsRequired();
            modelBuilder.Entity<Building>().Property(e => e.Location).HasColumnType("VARCHAR(50)");
            modelBuilder.Entity<Building>().Property(e => e.Id).HasColumnType("smallint").ValueGeneratedNever().IsRequired();
            
            modelBuilder.Entity<BuildingObject>().Property(e => e.Name).HasColumnType("VARCHAR(50)").IsRequired();
            modelBuilder.Entity<BuildingObject>().Property(e => e.Id).HasColumnType("tinyint").ValueGeneratedNever().IsRequired();
            
            modelBuilder.Entity<BuildingDataField>().Property(e => e.Name).HasColumnType("VARCHAR(50)").IsRequired();
            modelBuilder.Entity<BuildingDataField>().Property(e => e.Id).ValueGeneratedNever().HasColumnType("tinyint");
            
            modelBuilder.Entity<Reading>().Property(e => e.BuildingEntityId).HasColumnType("smallint").ValueGeneratedNever().IsRequired();
            modelBuilder.Entity<Reading>().Property(e => e.ObjectEntityId).HasColumnType("tinyint").ValueGeneratedNever().IsRequired();
            modelBuilder.Entity<Reading>().Property(e => e.DataFieldEntityId).HasColumnType("tinyint").ValueGeneratedNever().IsRequired();
            modelBuilder.Entity<Reading>().Property(e => e.Timestamp).IsRequired();
            modelBuilder.Entity<Reading>().Property(e => e.Value).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<Reading>().HasIndex(e => e.BuildingEntityId);
            modelBuilder.Entity<Reading>().HasIndex(e => e.ObjectEntityId);
            modelBuilder.Entity<Reading>().HasIndex(e => e.DataFieldEntityId);
            modelBuilder.Entity<Reading>().HasIndex(e => e.Timestamp);
            
            

            base.OnModelCreating(modelBuilder);
        }
    }
}