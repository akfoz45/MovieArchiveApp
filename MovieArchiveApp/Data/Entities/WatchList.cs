using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class WatchList
    {
        public int Id { get; set; }

        // İzledi mi? (Opsiyonel özellik)
        public bool IsWatched { get; set; } = false;

        // İlişkiler
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}