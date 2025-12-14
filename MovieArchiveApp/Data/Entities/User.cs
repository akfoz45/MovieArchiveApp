// Data/Entities/User.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieArchiveApp.Data.Entities
{
    public class User // This class defines the User table structure.
    {
        [Key] // This is the primary key for the table.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The ID value is generated automatically by the database.
        public int Id { get; set; } // The unique user ID.

        [Required] // This field must be filled.
        public string Username { get; set; } = string.Empty; // The user's chosen username.

        [Required] // This field must be filled.
        public string PasswordHash { get; set; } = string.Empty; // The hashed and secure version of the password.

        public bool IsAdmin { get; set; } = false; // A flag to check if the user is an administrator.

        public List<WatchList> WatchLists { get; set; } = new(); // Navigation property: A list of the user's watch list entries.
    }
}