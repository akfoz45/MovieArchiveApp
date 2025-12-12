// Data/Entities/User.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieArchiveApp.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty; 

        [Required]
        public string PasswordHash { get; set; } = string.Empty; 

        public bool IsAdmin { get; set; } = false; 

        public List<WatchList> WatchLists { get; set; } = new();
    }
}