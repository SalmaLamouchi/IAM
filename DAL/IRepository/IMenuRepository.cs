using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IMenuRepositoryAsync
    {
        Task<IEnumerable<Menu>> GetMenusByRoleAsync(int roleId);
    }
}
