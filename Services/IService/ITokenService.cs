using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Services.DTO;

namespace Services.IService
{
    public interface ITokenService
    {
        string GenerateToken(UtilisateurDto utilisateur);
        bool IsTokenRevoked(string token);
        void RevokeToken(string token);
    }

  
}
