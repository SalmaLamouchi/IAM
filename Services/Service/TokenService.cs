using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Microsoft.IdentityModel.Tokens;
using Services.DTO;
using Services.IService;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Service
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly HashSet<string> _revokedTokens = new HashSet<string>();

        public TokenService(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(UtilisateurDto utilisateur)
        {
            // Log the user details
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, utilisateur.IdUser .ToString()),
        new Claim(JwtRegisteredClaimNames.Email, utilisateur.Email),
        new Claim("role", utilisateur.IdRole.ToString())
    };

            // Log claims
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourissuer",
                audience: "youraudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            // Log the generated token
            return tokenString;
        }

        public bool IsTokenRevoked(string token)
        {
            return _revokedTokens.Contains(token);
        }

        public void RevokeToken(string token)
        {
            _revokedTokens.Add(token);
        }

    }
}
