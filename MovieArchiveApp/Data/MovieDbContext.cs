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
        public DbSet<WatchList> WatchLists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(localAppData, "MovieArchiveApp");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            string dbPath = Path.Combine(appFolder, "MovieArchiveApp.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sonra doldur unutma!!
        }
    }
}
