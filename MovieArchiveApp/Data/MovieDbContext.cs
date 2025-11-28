using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Data
{
    public class MovieDbContext : DbContext
    {
        // 1. BOŞ CONSTRUCTOR (Senin kodların için gerekli)
        public MovieDbContext()
        {
        }

        // 2. AYARLI CONSTRUCTOR (Arkadaşının DI yapısı için gerekli)
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        // DİKKAT: İsim standardı için 'Rating' değil 'Ratings' (Çoğul) yaptım.
        // Senin servislerinde _db.Ratings olarak geçiyor çünkü.
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<WatchList> WatchLists { get; set; }

        // 3. BAĞLANTI AYARI (Senin kodların için gerekli)
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
            modelBuilder.Entity<WatchList>()
                .HasOne<User>() // User nesnesiyle ilişki
                .WithMany(u => u.WatchLists) // User'ın WatchLists listesiyle eşleşir
                .HasForeignKey(wl => wl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

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