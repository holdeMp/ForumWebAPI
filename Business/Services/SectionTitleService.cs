using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL.Interfaces;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SectionTitleService : ISectionTitleService
    {
        private readonly ISectionTitleRepository sectionTitleRep;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SectionTitleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.sectionTitleRep = unitOfWork.SectionTitleRepository;
            this.mapper = mapper;
        }
        public async Task AddAsync(SectionTitleModel model)
        {
            if (model.Name == "" || GetAll().Any(i => i.Name == model.Name))
            {
                throw new ForumException("Incorrect name");
            }
            var section = mapper.Map<SectionTitleModel, SectionTitle>(model);
            await unitOfWork.SectionTitleRepository.AddAsync(section);
            await unitOfWork.SaveAsync();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SectionTitleModel> GetAll()
        {
            var sectionsTitle = unitOfWork.SectionTitleRepository.FindAll();
            var sectionsTitleModels = new List<SectionTitleModel>();
            foreach (var sectionTitle in sectionsTitle)
            {
                sectionsTitleModels.Add(mapper.Map<SectionTitle, SectionTitleModel>(sectionTitle));
            }

            return sectionsTitleModels;
        }


        public async Task UpdateAsync(SectionTitleModel model)
        {
            if (model.Name == null )
            {
                throw new ForumException("Incorrect name");
            }
            sectionTitleRep.Update(mapper.Map<SectionTitleModel, SectionTitle>(model));
            await unitOfWork.SaveAsync();
        }

        async Task<SectionTitleModel> ICrud<SectionTitleModel>.GetByIdAsync(int id)
        {
           var sectionTitle = await sectionTitleRep.GetByIdAsync(id);
           return mapper.Map<SectionTitle, SectionTitleModel>(sectionTitle);
        }
        
    }
}
