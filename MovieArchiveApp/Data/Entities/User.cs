using System.ComponentModel.DataAnnotations;

namespace MovieArchiveApp.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        //public bool IsAdmin { get; set; } = false;

        public List<WatchList> WatchLists { get; set; } = new();
    }
}