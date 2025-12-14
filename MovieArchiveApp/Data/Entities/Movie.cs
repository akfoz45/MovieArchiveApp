using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class Movie // This class defines the Movie table structure in the database.
    {
        public int Id { get; set; } // This is the unique identifier (Primary Key) for the movie.

        [Required] // This means the title field must be filled.
        public string Title { get; set; } = string.Empty; // The title of the movie (e.g., The Matrix).
        public string Description { get; set; } = string.Empty; // A short summary of the movie.
        public int ReleaseYear { get; set; } // The year the movie was released.
        public string Director { get; set; } = string.Empty; // The name of the movie director.
        public string Genre { get; set; } = string.Empty; // The genre of the movie (e.g., Action, Drama).
        public string PosterPath { get; set; } = string.Empty; // The path or URL to the movie poster image.
        public List<Rating> Ratings { get; set; } = new(); // Navigation property: A list of all ratings for this movie.
    }
}