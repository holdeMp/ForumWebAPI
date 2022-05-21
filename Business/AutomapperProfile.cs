using AutoMapper;
using Business.Models;
using DAL.Entities;

namespace Business
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile() {
            CreateMap<Section, SectionModel>().ReverseMap();
            CreateMap<SectionTitle, SectionTitleModel>().ReverseMap();
            CreateMap<SubSection, SubSectionModel>().ReverseMap();
            CreateMap<ThemeModel, AddThemeModel>().ReverseMap();
            CreateMap<ThemeModel, Theme>().ReverseMap();
            CreateMap<AnswerModel, Answer>().ReverseMap();
        }
    }
}
