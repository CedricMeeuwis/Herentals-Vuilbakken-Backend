using HerentalsVuilbakken.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Data
{
    public class VuilbakContext : DbContext
    {
        public VuilbakContext(DbContextOptions<VuilbakContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Vuilbak> Vuilbakken { get; set; }
        public DbSet<VuilbakLogging> VuilbakLoggings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Enquete> Enquetes { get; set; }
        public DbSet<Antwoord> Antwoorden { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Vuilbak>().ToTable("Vuilbak");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Zone>().ToTable("Zone");
            modelBuilder.Entity<VuilbakLogging>().ToTable("VuilbakLogging");
            modelBuilder.Entity<Enquete>().ToTable("Enquete");
            modelBuilder.Entity<Antwoord>().ToTable("Antwoord");

            modelBuilder.Entity<Vuilbak>()
                .HasMany(v => v.VuilbakLoggings)
                .WithOne(v => v.Vuilbak)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Zone>()
                .HasMany(v => v.Vuilbakken)
                .WithOne(v => v.Zone)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Enquete>()
                .HasMany(x => x.Antwoorden)
                .WithOne(x => x.Enquete)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
