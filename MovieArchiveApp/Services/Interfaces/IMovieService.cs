using MovieArchiveApp.Data.Entities;
using System.Collections.Generic;

namespace MovieArchiveApp.Services.Interfaces
{
    public interface IMovieService // This interface defines the contract for movie operations (CRUD).
    {
        List<Movie> GetAllMovies(); // Function to get all movies.
        List<Movie> SearchMovies(string query); // Function to search movies by text.

        void AddMovie(Movie movie); // Function to add a new movie (used by the 'Add' button).
        void UpdateMovie(Movie movie); // Function to change movie details (used by the 'Edit' button).
        void DeleteMovie(int id); // Function to delete a movie by ID (used by the 'Delete' button).
    }
}