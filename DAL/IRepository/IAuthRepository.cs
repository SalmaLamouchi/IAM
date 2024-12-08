using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.IRepository

{
    
        public interface IAuthRepository
        {

        Task<Utilisateur?> GetUserByEmailAsync(string email);
            Task<Utilisateur?> GetUserByIdAsync(int id);
            Task AddUserAsync(Utilisateur utilisateur);
            Task SaveAsync();
        }
    }

