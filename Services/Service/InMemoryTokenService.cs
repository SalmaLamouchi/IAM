using Microsoft.IdentityModel.Tokens;
using Services.DTO;
using Services.IService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
   
        public class InMemoryTokenService : ITokenService
        {
            // In-memory storage for revoked tokens
            private readonly HashSet<string> _revokedTokens = new HashSet<string>();

     
            private readonly string _secretKey;

            public InMemoryTokenService(string secretKey)
            {
                _secretKey = secretKey;
            }

            // Method to generate JWT token
            public string GenerateToken(UtilisateurDto utilisateur)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, utilisateur.IdUser.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, utilisateur.Email),
                new Claim("role", utilisateur.IdRole.ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "yourissuer",
                    audience: "youraudience",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            // Method to check if token is revoked
            public bool IsTokenRevoked(string token)
            {
                return _revokedTokens.Contains(token);
            }

            // Method to revoke a token
            public void RevokeToken(string token)
            {
                _revokedTokens.Add(token);
            }
        }
    }
