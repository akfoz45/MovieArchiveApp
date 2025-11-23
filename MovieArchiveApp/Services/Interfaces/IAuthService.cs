// Services/Interfaces/IAuthService.cs

using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Services.Interfaces
{
    public interface IAuthService
    {
        //  Düzeltildi: Sadece işlevi belirten 'SignUp' kullanıldı.
        User? SignUp(string username, string password, bool isAdmin);

        User? Login(string username, string password);  

        void Logout();
    }
}