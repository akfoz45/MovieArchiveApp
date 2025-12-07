using Microsoft.EntityFrameworkCore;
using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieArchiveApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _db;

        // Dependency Injection (DI) için constructor
        public MovieService(MovieDbContext db)
        {
            _db = db;
        }

        // --- READ (OKUMA) METOTLARI ---

        public List<Movie> GetAllMovies()
        {
            // Tüm filmleri döndürür.
            return _db.Movies.ToList();
        }

        public List<Movie> SearchMovies(string query)
        {
            // İsim veya tür alanında arama yapar.
            if (string.IsNullOrWhiteSpace(query))
            {
                return GetAllMovies();
            }

            string lowerQuery = query.ToLower();

            return _db.Movies
                .Where(m => m.Title.ToLower().Contains(lowerQuery) || m.Genre.ToLower().Contains(lowerQuery))
                .ToList();
        }

        // --- CREATE (EKLEME) METODU ---

        public void AddMovie(Movie movie)
        {
            // Yeni filmi veritabanına ekler.
            _db.Movies.Add(movie);
            _db.SaveChanges();
        }

        // --- UPDATE (GÜNCELLEME) METODU ---

        public void UpdateMovie(Movie movie)
        {
            // Mevcut filmi veritabanında günceller.
            // EF Core, bu nesnenin durumunu (State) otomatik olarak 'Modified' yapar.
            _db.Movies.Update(movie);
            _db.SaveChanges();
        }

        // --- DELETE (SİLME) METODU ---

        public void DeleteMovie(int id)
        {
            // Önce silinecek filmi ID'ye göre bulur.
            var movie = _db.Movies.Find(id);

            if (movie != null)
            {
                // Filmi veritabanından siler.
                _db.Movies.Remove(movie);
                _db.SaveChanges();
            }
        }
    }
}