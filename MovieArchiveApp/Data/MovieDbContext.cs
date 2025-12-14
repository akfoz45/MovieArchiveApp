using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Data
{
    public class MovieDbContext : DbContext // This class is the connection to the database.
    {
        // 1. BOŞ CONSTRUCTOR (EMPTY CONSTRUCTOR)
        public MovieDbContext() // Default constructor.
        {
        }

        // 2. AYARLI CONSTRUCTOR (CONFIGURED CONSTRUCTOR)
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) // Constructor that receives options (used for DI).
        {
        }

        public DbSet<Movie> Movies { get; set; } // This is the Movies table in the database.
        public DbSet<User> Users { get; set; } // This is the Users table.

        public DbSet<Rating> Ratings { get; set; } // This is the Ratings table.

        public DbSet<WatchList> WatchLists { get; set; } // This is the Watch Lists table.

        // 3. BAĞLANTI AYARI (CONNECTION SETTING)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Function to configure the database connection.
        {
            // If settings did not come from outside (from Program.cs), this runs.
            if (!optionsBuilder.IsConfigured) // Check if connection options are set.
            {
                // Write your own connection string here (SQL Server example):
                //  optionsBuilder.UseSqlServer("Server=.;Database=MovieArchiveDb;Trusted_Connection=True;TrustServerCertificate=True;"); // This line sets the database connection.
            }
        }

        // 4. İLİŞKİLER (RELATIONSHIPS)
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Function to define table relationships.
        {
            // WatchList Relationship
            modelBuilder.Entity<WatchList>()
        .HasOne(wl => wl.User) // A WatchList item has one User.
                .WithMany(u => u.WatchLists) // A User has many WatchList items.
                .HasForeignKey(wl => wl.UserId) // User ID is the foreign key.
                .OnDelete(DeleteBehavior.Cascade); // Delete WatchList items when the User is deleted.

            modelBuilder.Entity<WatchList>()
        .HasOne(wl => wl.Movie) // A WatchList item has one Movie.
                .WithMany() // A Movie can be in many WatchLists.
                .HasForeignKey(wl => wl.MovieId) // Movie ID is the foreign key.
                .OnDelete(DeleteBehavior.Cascade); // Delete WatchList items when the Movie is deleted.

            // Rating Relationship
            modelBuilder.Entity<Rating>()
        .HasOne(r => r.Movie) // A Rating belongs to one Movie.
                .WithMany(m => m.Ratings) // A Movie has many Ratings.
                .HasForeignKey(r => r.MovieId) // Movie ID is the foreign key.
                .OnDelete(DeleteBehavior.Cascade); // Delete Ratings when the Movie is deleted.

            modelBuilder.Entity<Rating>()
        .HasOne(r => r.User) // A Rating belongs to one User.
                .WithMany() // A User can give many Ratings.
                .HasForeignKey(r => r.UserId) // User ID is the foreign key.
                .OnDelete(DeleteBehavior.Restrict); // Do not delete the User if there are ratings (restrict).

            base.OnModelCreating(modelBuilder); // Call the base implementation.
        }
    }
}