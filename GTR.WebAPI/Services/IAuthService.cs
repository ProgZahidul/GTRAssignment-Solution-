using GTR.WebAPI.Models;

namespace GTR.WebAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(AuthModel model);
        Task<AuthResponse> RegisterAsync(AuthRequest model);
    }
}
