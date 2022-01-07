using AutoMapper;
using Business.Models;
using DAL;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile() {
            CreateMap<Section, SectionModel>().ReverseMap();
            CreateMap<SectionTitle, SectionTitleModel>().ReverseMap();
            CreateMap<SubSection, SubSectionModel>().ReverseMap();
        }
    }
}
