using AutoMapper;
using BCrypt.Net;
using DAL.Entities;
using DAL.IRepository;
using Microsoft.CodeAnalysis.Scripting;
using Services.DTO;
using Services.IService;

namespace Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper; // Add IMapper for AutoMapper

        public AuthService(ITokenService tokenService, IAuthRepository authRepository, IMapper mapper)
        {
            _tokenService = tokenService;
            _authRepository = authRepository;
            _mapper = mapper; // Inject IMapper
        }

        // Updated LoginAsync method using email and password
        public async Task<string?> LoginAsync(string email, string password)
        {
            // Validate user
            var user = await _authRepository.GetUserByEmailAsync(email);
            if (user == null || user.Motdepasse != password)
            {
                return null; // Invalid credentials
            }

            // Use AutoMapper to map the user entity to the DTO
            var userDto = _mapper.Map<UtilisateurDto>(user);

            // Generate JWT token
            var token = _tokenService.GenerateToken(userDto);

            return token;
        }

        // New LoginAsync method using LoginRequest
        public async Task<string?> LoginAsync(LoginRequest request)
        {
            // Validate user credentials
            var user = await _authRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return null; // User not found
            }

            // Verify the password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Motdepasse))
            {
                return null; // Invalid password
            }

            // Use AutoMapper to map the user entity to the DTO
            var userDto = _mapper.Map<UtilisateurDto>(user);

            // Generate JWT token
            var token = _tokenService.GenerateToken(userDto);

            return token;
        }


        // Method to revoke the token
        public void RevokeToken(string token)
        {
            _tokenService.RevokeToken(token); // Add token to revoked list
        }

        // Check if the token has been revoked
        public bool IsTokenRevoked(string token)
        {
            return _tokenService.IsTokenRevoked(token);
        }

        // Placeholder methods (could be implemented later)
        public Task<UtilisateurDto?> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task RevokeTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
