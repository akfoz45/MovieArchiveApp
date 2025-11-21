using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using BCrypt.Net;
using System.Linq;

namespace MovieArchiveApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly MovieDbContext _context;

        public AuthService()
        {
            _context = new MovieDbContext();
        }

        public User? Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return user;
        }

        public bool Register(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
                return false; 

            var newUser = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }
    }
}