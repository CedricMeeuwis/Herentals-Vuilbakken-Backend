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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Vuilbak>().ToTable("Vuilbak");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Zone>().ToTable("Zone");
            modelBuilder.Entity<VuilbakLogging>().ToTable("VuilbakLogging");
        }
    }
}
