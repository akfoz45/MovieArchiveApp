using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Core
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}