using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MovieArchiveApp.Services
{
    public class WatchListService
    {
        private readonly MovieDbContext _db;

        public WatchListService(MovieDbContext db)
        {
            _db = db;
        }

        // 1. Listeye Ekle
        public bool AddToWatchlist(int userId, int movieId)
        {
            // Zaten ekli mi kontrol et
            bool exists = _db.WatchLists.Any(w => w.UserId == userId && w.MovieId == movieId);

            if (exists) return false; // Ekliyse işlem yapma

            var item = new WatchList
            {
                UserId = userId,
                MovieId = movieId,
            };

            _db.WatchLists.Add(item);
            _db.SaveChanges();
            return true;
        }

        // 2. Listeden Çıkar
        public void RemoveFromWatchlist(int userId, int movieId)
        {
            var item = _db.WatchLists.FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId);

            if (item != null)
            {
                _db.WatchLists.Remove(item);
                _db.SaveChanges();
            }
        }

        // 3. Kullanıcının Listesini Getir
        public List<Movie> GetUserWatchlist(int userId)
        {
            return _db.WatchLists
                      .Where(w => w.UserId == userId)
                      .Include(w => w.Movie)
                      .Select(w => w.Movie)
                      .ToList();
        }
    }
}