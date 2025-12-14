// Services/Interfaces/IAuthService.cs

using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Services.Interfaces
{
    public interface IAuthService // This interface defines the contract for authentication services.
    {
        // Corrected: Used 'SignUp' to define the functionality.
        User? SignUp(string username, string password, bool isAdmin); // Function for user registration.

        User? Login(string username, string password); // Function for user login.  

        void Logout(); // Function to end the user session (used by the 'Exit' button).
    }
}