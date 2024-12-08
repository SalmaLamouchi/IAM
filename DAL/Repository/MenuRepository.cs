using DAL.Entities;
using DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class MenuRepositoryAsync : IMenuRepositoryAsync
    {
        private readonly AuthDbContext _context;

        public MenuRepositoryAsync(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetMenusByRoleAsync(int roleId)
        {
            // Récupérer les menus associés au rôle spécifié
            var menus = await _context.Menus
                .Where(m => m.IdRoles.Any(r => r.IdRole == roleId))
                .ToListAsync();

            return menus;
        }
    }
}
