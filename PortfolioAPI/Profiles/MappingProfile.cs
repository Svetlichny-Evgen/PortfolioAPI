using AutoMapper;
using PortfolioAPI.Models;
using PortfolioAPI.DTOs;

namespace PortfolioAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
        CreateMap<User, UserDto>();
        CreateMap<Skill, SkillDto>()
            .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Category.Title));

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.TechTitles, opt => opt.MapFrom(src =>
                src.Techs.Select(t => t.Tech.Title))); 
        }
        
    }
}
