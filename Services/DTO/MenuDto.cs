using AutoMapper;
using DAL.Entities;
using Services.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public partial class MenuDto :IMapFrom<Menu>
    {
        public int IdMenu { get; set; }
        public string Titre { get; set; } = null!;
        public string? Description { get; set; }
        public string? Link { get; set; }
        public int? ParentId { get; set; }

        // Liste des sous-menus (inverseParent)
        public List<MenuDto> InverseParent { get; set; } = new List<MenuDto>();

        // Menu parent (facultatif, peut être nul)
        public MenuDto? Parent { get; set; }

        // Liste des rôles associés à ce menu
        public List<int> RoleIds { get; set; } = new List<int>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Menu, MenuDto>().ReverseMap();

        }
    }
}
    