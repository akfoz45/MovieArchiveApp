// Services/AuthService.cs
// AuthService.cs'in başındaki using'ler arasına ekleyin:
using BCrypt.Net;
using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using MovieArchiveApp.Services.Helpers;
namespace MovieArchiveApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly MovieDbContext _context;

        // ✅ KRİTİK: DI uyumlu Constructor. Program.cs'ten DbContext'i alır.
        public AuthService(MovieDbContext context)
        {
            _context = context;
        }

        // Kullanıcı Giriş Metodu
        public User? Login(string username, string password)
        {
            // 1. Kullanıcıyı bul (Case-Sensitive - ToLower() kaldırıldı)
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            // 2. Kontrol: Kullanıcı yoksa veya şifre yanlışsa null dön.
            // PasswordHash kullandığımıza emin olmalıyız.
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            // 3. Oturumu başlat
            SessionManager.Login(user);

            return user;
        }

        // Kullanıcı Kayıt Metodu
        public User? SignUp(string username, string password, bool isAdmin)
        {
            // 1. Kullanıcı adının benzersizliğini kontrol et (Case-Sensitive)
            if (_context.Users.Any(u => u.Username == username))
                return null; // Zaten varsa null (başarısız) döndür

            // 2. Yeni Kullanıcı nesnesini oluştur
            var newUser = new User
            {
                Username = username,
                // KRİTİK: Şifreyi hashleyerek PasswordHash özelliğine atıyoruz.
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                IsAdmin = isAdmin,
            };

            // 3. Veritabanına kaydet
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        // Oturumu Kapatma Metodu
        public void Logout()
        {
            SessionManager.Logout();
        }
    }
}