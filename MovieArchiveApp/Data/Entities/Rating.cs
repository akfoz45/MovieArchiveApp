using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class Rating // This class defines the Rating table structure.
    {
        public int Id { get; set; } // The unique identifier for the rating entry.

        [Range(1, 10)] // This ensures the score is between 1 and 10.
        public int Score { get; set; } // The score given by the user (1 to 10).

        public int MovieId { get; set; } // Foreign Key: The ID of the movie being rated.
        public Movie Movie { get; set; } = null!; // Navigation property: The movie object.

        public int UserId { get; set; } // Foreign Key: The ID of the user who gave the rating.
        public User User { get; set; } = null!; // Navigation property: The user object.
    }
}