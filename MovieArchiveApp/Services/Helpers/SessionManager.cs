// Services/Helpers/SessionManager.cs dosyasının içeriği

using MovieArchiveApp.Data.Entities;
// Not: using'lerin doğru namespace'i göstermesi için kontrol edin.
// using MovieArchiveApp.Core; yerine belki de using MovieArchiveApp.Services.Helpers; yazmalısınız.

namespace MovieArchiveApp.Services.Helpers // Veya projenizin Helpers namespace'i
{
    public static class SessionManager // This class manages the user's session data.
    {
        public static User? CurrentUser { get; private set; } // This holds the user object when logged in.

        public static bool IsLoggedIn => CurrentUser != null; // Checks if a user is currently logged in.

        public static void Login(User user) // This function starts the session.
        {
            CurrentUser = user; // Set the current user.
        }

        public static void Logout() // This function ends the session (used by the 'Exit' button).
        {
            CurrentUser = null; // Clear the current user.
        }

        public static bool IsAdmin => CurrentUser?.IsAdmin ?? false; // Checks if the logged-in user is an admin.


        // --- EKLENECEK TEK SATIR (KÖPRÜ) ---
        // This line is for easily getting the user's ID.
        // If the user is null, it returns 0 to prevent errors.
        public static int CurrentUserId => CurrentUser?.Id ?? 0; // Gets the ID of the current user.
    }
}