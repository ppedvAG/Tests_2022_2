using Microsoft.EntityFrameworkCore;
using Stammdatenverwaltung.Model;

namespace Stammdatenverwaltung.Data
{
    public class EfContext : DbContext
    {
        private readonly string _conConstring;

        public DbSet<Abteilung> Abteilungen { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }

        public EfContext() : this("Data Source=StammDaten.db;")
        { }

        public EfContext(string conConstring)
        {
            _conConstring = conConstring;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_conConstring).UseLazyLoadingProxies();
        }
    }
}
