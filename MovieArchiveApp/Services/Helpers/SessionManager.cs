// Services/Helpers/SessionManager.cs dosyasının içeriği

using MovieArchiveApp.Data.Entities;
// Not: using'lerin doğru namespace'i göstermesi için kontrol edin.
// using MovieArchiveApp.Core; yerine belki de using MovieArchiveApp.Services.Helpers; yazmalısınız.

namespace MovieArchiveApp.Services.Helpers // Veya projenizin Helpers namespace'i
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsAdmin => CurrentUser?.IsAdmin ?? false;


        // --- EKLENECEK TEK SATIR (KÖPRÜ) ---
        // Bu satır sayesinde "SessionManager.CurrentUserId" dediğimizde
        // yukarıdaki kullanıcının ID'sini otomatik alacak.
        // Eğer kullanıcı yoksa (null), hata vermemek için 0 döndürecek.
        public static int CurrentUserId => CurrentUser?.Id ?? 0;
    }
}