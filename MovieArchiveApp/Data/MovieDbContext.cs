using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Data
{
    public class MovieDbContext : DbContext
    {
        // 1. BOŞ CONSTRUCTOR 
        public MovieDbContext()
        {
        }

        // 2. AYARLI CONSTRUCTOR
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<WatchList> WatchLists { get; set; }

        // 3. BAĞLANTI AYARI 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Eğer dışarıdan (Program.cs'ten) ayar gelmediyse burası çalışır.
            if (!optionsBuilder.IsConfigured)
            {
                // Buraya kendi bağlantı cümleni yaz (SQL Server örneği):
              //  optionsBuilder.UseSqlServer("Server=.;Database=MovieArchiveDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        // 4. İLİŞKİLER (Arkadaşının yazdığı kurallar aynen duruyor)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WatchList İlişkisi 
            modelBuilder.Entity<WatchList>()
                .HasOne(wl => wl.User) 
                .WithMany(u => u.WatchLists)
                .HasForeignKey(wl => wl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchList>()
                .HasOne(wl => wl.Movie)
                .WithMany()
                .HasForeignKey(wl => wl.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rating İlişkisi
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}