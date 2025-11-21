using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Services.Interfaces
{
    public interface IAuthService
    {
        bool Register(string username, string password);
        User? Login(string username, string password);
    }
}
