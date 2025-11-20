using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data.Entities;
using System.IO;

using System.Runtime.InteropServices;

namespace MovieArchiveApp.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<WhatchList> WhatchLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Veri tabanının oluşturulacağı yer
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MovieArchiveApp.db");

            //Klasöre eklemek istersen, zorunlu değil
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sonra doldur unutma!!
        }
    }
}
