using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    using System.Threading.Tasks;
    using DAL.Entities;
    using Newtonsoft.Json.Linq;
    using NuGet.Common;

namespace DAL.IRepository
    {
        public interface ITokenRepository
        {
            Task AddTokenAsync(Token token);
            Task<Token?> GetTokenAsync(string token);
            Task<bool> IsTokenRevokedAsync(string token);
            Task RevokeTokenAsync(string token);
        }
    }
