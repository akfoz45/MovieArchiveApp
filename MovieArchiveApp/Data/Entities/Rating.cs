using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 10)]
        public int Score { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}