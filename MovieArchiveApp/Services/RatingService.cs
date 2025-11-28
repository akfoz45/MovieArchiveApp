using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using System.Linq;

namespace MovieArchiveApp.Services
{
    public class RatingService
    {
        private readonly MovieDbContext _db;

        public RatingService()
        {
            _db = new MovieDbContext();
        }

        // 1. Puan Ver veya Güncelle
        public void GiveRating(int userId, int movieId, int score)
        {
            var rating = _db.Ratings.FirstOrDefault(r => r.UserId == userId && r.MovieId == movieId);

            if (rating == null)
            {
                // İlk defa puan veriyor
                rating = new Rating { UserId = userId, MovieId = movieId, Score = score };
                _db.Ratings.Add(rating);
            }
            else
            {
                // Zaten vermiş, puanını değiştiriyor
                rating.Score = score;
            }
            _db.SaveChanges();
        }

        // 2. Filmin Ortalama Puanını Getir (Örn: 8.4)
        public double GetAverageRating(int movieId)
        {
            var ratings = _db.Ratings.Where(r => r.MovieId == movieId).ToList();

            if (ratings.Count == 0) return 0; // Hiç puan yoksa 0 döndür

            return ratings.Average(r => r.Score);
        }

        // 3. Kullanıcının Kendi Puanını Getir (Ekranda göstermek için)
        public int GetUserScore(int userId, int movieId)
        {
            var rating = _db.Ratings.FirstOrDefault(r => r.UserId == userId && r.MovieId == movieId);
            return rating != null ? rating.Score : 0;
        }
    }
}