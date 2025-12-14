using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MovieArchiveApp.Services
{
    public class WatchListService // This service manages the user's Watch List data.
    {
        private readonly MovieDbContext _db; // This is the database context object.

        public WatchListService(MovieDbContext db) // Constructor to get the database context.
        {
            _db = db; // Set the database context.
        }

        // 1. Add to List (used by the 'Add to Watch List' button)
        public bool AddToWatchlist(int userId, int movieId) // Function to add a movie to the list.
        {
            // Check if it is already added.
            bool exists = _db.WatchLists.Any(w => w.UserId == userId && w.MovieId == movieId); // Check if the item exists.

            if (exists) return false; // If it exists, do nothing and return false.

            var item = new WatchList // Create a new WatchList entry.
            {
                UserId = userId, // Set the user ID.
                MovieId = movieId, // Set the movie ID.
            };

            _db.WatchLists.Add(item); // Add the new item to the context.
            _db.SaveChanges(); // Save changes to the database.
            return true; // Return true for success.
        }

        // 2. Remove from List (used by the 'Remove from List' button)
        public void RemoveFromWatchlist(int userId, int movieId) // Function to remove a movie from the list.
        {
            var item = _db.WatchLists.FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId); // Find the item to remove.

            if (item != null) // Check if the item was found.
            {
                _db.WatchLists.Remove(item); // Remove the item from the context.
                _db.SaveChanges(); // Save changes to the database.
            }
        }

        // 3. Get User's List
        public List<Movie> GetUserWatchlist(int userId) // Function to get all movies in a user's list.
        {
            return _db.WatchLists // Look in the WatchLists table.
                            .Where(w => w.UserId == userId) // Filter by user ID.
                            .Include(w => w.Movie) // Also include the Movie details (join).
                            .Select(w => w.Movie) // Select only the Movie objects.
                            .ToList(); // Convert the result to a list.
        }
    }
}