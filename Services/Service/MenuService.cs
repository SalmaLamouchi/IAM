using AutoMapper;
using DAL.Entities;
using DAL.IRepository;
using Services.DTO;
using Services.IService;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Services.Service
{
    public class MenuService : ServiceAsync<Menu, MenuDto>, IMenuService
    {
        //private readonly IMenuRepositoryAsync menuRepository;
        private readonly IRepositoryAsync<Menu> menuRepository;
        private readonly IServiceAsync<Menu, MenuDto> service;
        private readonly IMapper mapper;
        private readonly IMenuService srvMenu;




        public MenuService(IRepositoryAsync<Menu> _menuRepository,
                   IMapper _mapper,
                   IServiceAsync<Menu, MenuDto> _service)
    : base(_menuRepository, _mapper)
        {
            mapper = _mapper;
            menuRepository = _menuRepository;
            service = _service;
        }


        public Task<IEnumerable<MenuDto>> GetMenuByIdRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<MenuDto> GetMenuWithRolesById(int menuId)
        {
            var menu = await menuRepository.GetFirstOrDefault(
                predicate: m => m.IdMenu == menuId,
                include: m => m
                    .Include(menu => menu.IdRoles)
                    .Include(menu => menu.InverseParent),
                disableTracking: true
            );

            if (menu != null)
            {
                var menuDto = mapper.Map<MenuDto>(menu);

                if (menu.IdRoles != null && menu.IdRoles.Any())
                {
                    menuDto.RoleIds = menu.IdRoles.Select(role => role.IdRole).ToList();
                }

                return menuDto;
            }

            return null;
        }


    }
}
