using BCrypt.Net;
using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using MovieArchiveApp.Services.Helpers;
namespace MovieArchiveApp.Services
{
    public class AuthService : IAuthService // This service handles all authentication (login/signup) actions.
    {
        private readonly MovieDbContext _context; // This is the database context.

        public AuthService(MovieDbContext context) // Constructor with database context injected.
        {
            _context = context; // Set the database context.
        }

        // User Login Method
        public User? Login(string username, string password) // Function to log in a user.
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username); // Find the user by username.

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) // Check if user exists or if password is correct.
                return null; // Return null if login fails.

            SessionManager.Login(user); // Start the user session.

            return user; // Return the user object if successful.
        }

        // User Sign Up Method
        public User? SignUp(string username, string password, bool isAdmin) // Function to register a new user.
        {
            // 1. Check if the username is unique.
            if (_context.Users.Any(u => u.Username == username)) // Check if a user with this username exists.
                return null; // Return null if the username is already taken.

            // 2. Create a new User object.
            var newUser = new User // Create a new user.
            {
                Username = username, // Set the username.
                // CRITICAL: Hash the password and save it as PasswordHash.
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password), // Hash the password for security.
                IsAdmin = isAdmin, // Set the admin status.
            };

            // 3. Save to the database.
            _context.Users.Add(newUser); // Add the new user to the context.
            _context.SaveChanges(); // Save changes to the database.

            return newUser; // Return the new user object.
        }

        // Oturumu Kapatma Metodu (Logout Method - used by the 'Exit' button)
        public void Logout() // Function to log out the user.
        {
            SessionManager.Logout(); // Call the SessionManager to end the session.
        }
    }
}