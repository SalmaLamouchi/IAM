using Services.DTO;

namespace Services.IService
{
    public interface IAuthService
    {
        Task<UtilisateurDto?> AuthenticateAsync(string email, string password);
        Task<string> LoginAsync(LoginRequest request); // Méthode pour se connecter
        Task<string> RegisterAsync(RegisterRequest request);
        Task RevokeTokenAsync(string token);
    }
}
