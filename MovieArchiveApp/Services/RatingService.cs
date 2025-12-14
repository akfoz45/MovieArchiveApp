using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using System.Linq;

namespace MovieArchiveApp.Services
{
    public class RatingService // This service manages movie rating operations.
    {
        private readonly MovieDbContext _db; // This is the database context.

        public RatingService() // Default constructor.
        {
            _db = new MovieDbContext(); // Initialize the database context (Note: often done with DI).
        }

        // 1. Puan Ver veya Güncelle (Give Rating or Update)
        public void GiveRating(int userId, int movieId, int score) // This function lets a user rate a movie.
        {
            var rating = _db.Ratings.FirstOrDefault(r => r.UserId == userId && r.MovieId == movieId); // Find the user's existing rating.

            if (rating == null) // If no rating is found.
            {
                // İlk defa puan veriyor (First time rating)
                rating = new Rating { UserId = userId, MovieId = movieId, Score = score }; // Create a new rating object.
                _db.Ratings.Add(rating); // Add the new rating to the database context.
            }
            else // If a rating already exists.
            {
                // Zaten vermiş, puanını değiştiriyor (User gave a score before, now changing it)
                rating.Score = score; // Update the score.
            }
            _db.SaveChanges(); // Save all changes to the database.
        }

        // 2. Filmin Ortalama Puanını Getir (Örn: 8.4) (Get Movie's Average Rating)
        public double GetAverageRating(int movieId) // This function calculates the average score for a movie.
        {
            var ratings = _db.Ratings.Where(r => r.MovieId == movieId).ToList(); // Get all ratings for this movie.

            if (ratings.Count == 0) return 0; // Return 0 if there are no ratings.

            return ratings.Average(r => r.Score); // Calculate and return the average score.
        }

        // 3. Kullanıcının Kendi Puanını Getir (Ekranda göstermek için) (Get User's Own Score)
        public int GetUserScore(int userId, int movieId) // This function gets the score given by a specific user.
        {
            var rating = _db.Ratings.FirstOrDefault(r => r.UserId == userId && r.MovieId == movieId); // Find the user's specific rating.
            return rating != null ? rating.Score : 0; // Return the score or 0 if not found.
        }
    }
}