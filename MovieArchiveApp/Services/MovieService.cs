using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieArchiveApp.Services
{
    public class MovieService : IMovieService // This service manages movie database operations.
    {
        private readonly MovieDbContext _db; // This is the database context object.

        // Dependency Injection (DI) için constructor
        public MovieService(MovieDbContext db) // Constructor to get the database context.
        {
            _db = db; // Set the database context.
        }

        // --- READ METHODS ---

        public List<Movie> GetAllMovies() // This function gets all movies.
        {
            // Returns all movies.
            return _db.Movies.ToList(); // Get all movies from the database.
        }

        public List<Movie> SearchMovies(string query) // This function searches for movies by title or genre.
        {
            // Searches in the name or genre field.
            if (string.IsNullOrWhiteSpace(query)) // If the search query is empty.
            {
                return GetAllMovies(); // Return all movies.
            }

            string lowerQuery = query.ToLower(); // Convert the query to lower case.

            return _db.Movies // Search in the Movies table.
                .Where(m => m.Title.ToLower().Contains(lowerQuery) || m.Genre.ToLower().Contains(lowerQuery)) // Filter by title or genre.
                .ToList(); // Convert the result to a list.
        }

        // --- CREATE METHOD ---

        public void AddMovie(Movie movie) // This function adds a new movie (used by the 'Add' button).
        {
            // Adds the new movie to the database.
            _db.Movies.Add(movie); // Add the movie object to the context.
            _db.SaveChanges(); // Save changes to the database.
        }

        // --- UPDATE METHOD ---

        public void UpdateMovie(Movie movie) // This function updates an existing movie (used by the 'Edit' button).
        {
            // Updates the existing movie in the database.
            // EF Core automatically sets the State of this object to 'Modified'.
            _db.Movies.Update(movie); // Mark the movie object for update.
            _db.SaveChanges(); // Save changes to the database.
        }

        // --- DELETE METHOD ---

        public void DeleteMovie(int id) // This function deletes a movie by its ID (used by the 'Delete' button).
        {
            // First, find the movie to be deleted by ID.
            var movie = _db.Movies.Find(id); // Find the movie.

            if (movie != null) // Check if the movie was found.
            {
                // Delete the movie from the database.
                _db.Movies.Remove(movie); // Remove the movie from the context.
                _db.SaveChanges(); // Save changes to the database.
            }
        }
    }
}