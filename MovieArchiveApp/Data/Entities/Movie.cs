using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Genre {  get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public List<Rating> Ratings { get; set; } = new();
    }
}
