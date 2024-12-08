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
    public class RoleDto : IMapFrom<Menu>
    {
        public int IdRole { get; set; }
        public int IdProfil { get; set; }
        public string TypeRole { get; set; } = null!;
   


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDto>().ReverseMap();

        }
    }
}
