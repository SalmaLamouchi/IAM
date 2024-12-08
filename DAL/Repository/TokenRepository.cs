using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.IRepository;
using NuGet.Common;

namespace DAL.Repository
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; }
    }

    public class TokenRepository : ITokenRepository
    {
        private readonly DbContext _context;

        public TokenRepository(DbContext context)
        {
            _context = context;
        }

        public async Task AddTokenAsync(Token token)
        {
            await _context.Set<Token>().AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public Task AddTokenAsync(NuGet.Common.Token token)
        {
            throw new NotImplementedException();
        }

        public async Task<Token?> GetTokenAsync(string token)
        {
            return await _context.Set<Token>().FirstOrDefaultAsync(t => t.Value == token);
        }

        public async Task<bool> IsTokenRevokedAsync(string token)
        {
            var dbToken = await GetTokenAsync(token);
            return dbToken != null && dbToken.IsRevoked;
        }

        public async Task RevokeTokenAsync(string token)
        {
            var dbToken = await GetTokenAsync(token);
            if (dbToken != null)
            {
                dbToken.IsRevoked = true;
                _context.Update(dbToken);
                await _context.SaveChangesAsync();
            }
        }

        Task<NuGet.Common.Token?> ITokenRepository.GetTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
