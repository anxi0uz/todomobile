using todomobile.Models;

namespace todomobile.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> Login(AuthRequest request);
    }
}