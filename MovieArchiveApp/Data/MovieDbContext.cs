// Data/MovieDbContext.cs

using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data.Entities;
// Artık System.IO ve System.Runtime.InteropServices gibi using'ler gerekmiyor
// çünkü bağlantı ayarları (SQLite yolu) Program.cs'te yapılıyor.

namespace MovieArchiveApp.Data
{
    public class MovieDbContext : DbContext
    {
        // 1. KRİTİK: DI uyumlu Constructor. Program.cs'ten gelen ayarları kabul eder.
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }

        // SİLİNMİŞ: OnConfiguring metodu (DI kullanıldığı için kaldırıldı.)
        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ... Eski bağlantı kodu buradaydı. ...
        } */

        // 2. KRİTİK: İlişkilerin Tanımlanması
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User ve WatchList İlişkisi (Bire-Çok)
            // Bir Kullanıcı silinirse, ona ait tüm izleme listesi kayıtları da silinir (Cascade).
            modelBuilder.Entity<WatchList>()
                .HasOne<User>()
                .WithMany(u => u.WatchLists)
                .HasForeignKey(wl => wl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Movie ve Rating İlişkisi (Bire-Çok)
            // Bir Film silinirse, ona ait tüm puanlama kayıtları da silinir (Cascade).
            modelBuilder.Entity<Rating>()
                .HasOne<Movie>()
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ve Rating İlişkisi (Bire-Çok)
            // Bir Kullanıcı silinirse, verdiği puanlar tutulmaya devam etsin (Restrict/NoAction).
            // Bu bir tasarım kararıdır. Puan kaydında sadece UserId null olur.
            modelBuilder.Entity<Rating>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}