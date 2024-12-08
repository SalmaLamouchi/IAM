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
    public class UtilisateurDto : IMapFrom<Utilisateur>
    {
        public int IdUser { get; set; }

        public string Email { get; set; } = null!;

        public string Motdepasse { get; set; } = null!;

        public int IdRole { get; set; }

        public string? Matricule { get; set; }
        public string Token { get; set; }
        public void Mapping(Profile profile)
        {
            // Exclude the Token property during mapping
            profile.CreateMap<Utilisateur, UtilisateurDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(src => src.Token, opt => opt.Ignore())
                .ReverseMap();
                
        }
    }
}
