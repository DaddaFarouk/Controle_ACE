using Controle_ACE_2.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Controle_ACE_2.Data
{
    public class DbSalarieContext : DbContext
    {
        public DbSalarieContext(DbContextOptions<DbSalarieContext> options) : base(options)
        {
        }
        public DbSet<Departement> Departement { get; set; }
        public DbSet<Salarie> Salarie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salarie>().ToTable("Salarie");
            modelBuilder.Entity<Departement>()
                        .ToTable("Departement")
                        .HasMany<Salarie>(s => s.Salaries);
        }
    }
}
