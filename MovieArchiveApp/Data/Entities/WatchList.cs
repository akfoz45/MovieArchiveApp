using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class WatchList // This class defines the WatchList table structure (links User and Movie).
    {
        public int Id { get; set; } // The unique identifier for this link/entry.

        public int UserId { get; set; } // Foreign Key: The ID of the user who owns this list item.
        public User User { get; set; } = null!; // Navigation property: The user object.

        public int MovieId { get; set; } // Foreign Key: The ID of the movie in the list.
        public Movie Movie { get; set; } = null!; // Navigation property: The movie object.
    }
}