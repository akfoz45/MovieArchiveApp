using MovieArchiveApp.Data.Entities;
using System.Collections.Generic;

namespace MovieArchiveApp.Services.Interfaces
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        List<Movie> SearchMovies(string query);

        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int id);
    }
}