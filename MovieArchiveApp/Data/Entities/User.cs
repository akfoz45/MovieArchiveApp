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
        public string Username { get; set; } = string.Empty; // <-- CS0117 Düzeltmesi: Username

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // <-- CS0117 Düzeltmesi: PasswordHash

        public bool IsAdmin { get; set; } = false; // <-- CS0117 Düzeltmesi: IsAdmin

        // İlişki: WatchLists de burada olmalı.
        public List<WatchList> WatchLists { get; set; } = new();
    }
}