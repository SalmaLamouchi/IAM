using Services.DTO;
using System;
using DAL.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IMenuService : IServiceAsync<Menu, MenuDto>
    {

        Task<IEnumerable<MenuDto>> GetMenuByIdRoleAsync(int roleId);
        Task<MenuDto> GetMenuWithRolesById(int menuId);
    }
}
