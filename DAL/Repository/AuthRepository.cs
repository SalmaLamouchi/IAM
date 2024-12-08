using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    using DAL.Entities;
    using global::DAL.Entities;
    using global::DAL.IRepository;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;


    namespace DAL.Repository
    {
        public class AuthRepository : IAuthRepository
        {
        private readonly AuthDbContext _context;
        // Constructeur injectant DbContext
        public AuthRepository(AuthDbContext context)
            {
                _context = context;
            }

            // Récupérer un utilisateur par email
            public async Task<Utilisateur?> GetUserByEmailAsync(string email)
            {
                return await _context.Set<Utilisateur>().FirstOrDefaultAsync(u => u.Email == email);
            }

            // Récupérer un utilisateur par ID
            public async Task<Utilisateur?> GetUserByIdAsync(int id)
            {
                return await _context.Set<Utilisateur>().FindAsync(id);
            }

            // Ajouter un utilisateur à la base de données
            public async Task AddUserAsync(Utilisateur utilisateur)
            {
                await _context.Set<Utilisateur>().AddAsync(utilisateur);
                await _context.SaveChangesAsync();
            }

            // Sauvegarder les changements dans la base de données
            public async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
    }
