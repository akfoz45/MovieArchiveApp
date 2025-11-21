using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class WatchList
    {
        public int Id { get; set; }

        public bool IsWatched { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
