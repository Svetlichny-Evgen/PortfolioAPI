using AutoMapper;
using TeamPortfolio.Models;
using TeamPortfolio.DTOs;

namespace TeamPortfolio.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Основной маппинг для TeamMember
            CreateMap<TeamMember, TeamMemberDTO>()
                .ForMember(dest => dest.SocialLinks, opt => opt.MapFrom(src => src.SocialLinks))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects));

            CreateMap<TeamMemberCreateDTO, TeamMember>();
            CreateMap<TeamMemberUpdateDTO, TeamMember>();

            // Маппинг для вложенных объектов
            CreateMap<Project, ProjectDTO>();
            CreateMap<SocialLinks, SocialLinksDTO>();
            CreateMap<Skills, SkillsDTO>();

            // Обратные маппинги (если нужны)
            CreateMap<TeamMemberDTO, TeamMember>();
            CreateMap<ProjectDTO, Project>();
            CreateMap<SocialLinksDTO, SocialLinks>();
            CreateMap<SkillsDTO, Skills>();
        }
    }
}