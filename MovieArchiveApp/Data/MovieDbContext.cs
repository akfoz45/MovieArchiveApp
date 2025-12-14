using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Data
{
    public class MovieDbContext : DbContext // This class is the connection to the database.
    {
        // EMPTY CONSTRUCTOR
        public MovieDbContext() // Default constructor.
        {
        }

        // CONFIGURED CONSTRUCTOR
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) // Constructor that receives options (used for DI).
        {
        }

        public DbSet<Movie> Movies { get; set; } // This is the Movies table in the database.
        public DbSet<User> Users { get; set; } // This is the Users table.

        public DbSet<WatchList> WatchLists { get; set; } // This is the Watch Lists table.

        // CONNECTION SETTING
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Function to configure the database connection.
        {
            // If settings did not come from outside (from Program.cs), this runs.
            if (!optionsBuilder.IsConfigured) // Check if connection options are set.
            {
                // Write your own connection string here (SQL Server example):
                //  optionsBuilder.UseSqlServer("Server=.;Database=MovieArchiveDb;Trusted_Connection=True;TrustServerCertificate=True;"); // This line sets the database connection.
            }
        }

        // RELATIONSHIPS
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

            base.OnModelCreating(modelBuilder); // Call the base implementation.
        }
    }
}