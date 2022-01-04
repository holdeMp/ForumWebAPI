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
        private readonly ISectionTitleRepository SectionTitleRep;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SectionTitleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.SectionTitleRep = unitOfWork.SectionTitleRepository;
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
            foreach (var SectionTitle in sectionsTitle)
            {
                sectionsTitleModels.Add(mapper.Map<SectionTitle, SectionTitleModel>(SectionTitle));
            }

            return sectionsTitleModels;
        }

        public async Task<SectionTitleModel> FindByName(string SectionTitleName)
        {
            var model = await SectionTitleRep.FindByNameAsync(SectionTitleName);
            return mapper.Map<SectionTitle, SectionTitleModel>(model);
        }
        public async Task UpdateAsync(SectionTitleModel model)
        {
            if (model.Name == null )
            {
                throw new ForumException("Incorrect name");
            }
            SectionTitleRep.Update(mapper.Map<SectionTitleModel, SectionTitle>(model));
            await unitOfWork.SaveAsync();
        }

        async Task<SectionTitleModel> ICrud<SectionTitleModel>.GetByIdAsync(int id)
        {
           var SectionTitle = await SectionTitleRep.GetByIdAsync(id);
           return mapper.Map<SectionTitle, SectionTitleModel>(SectionTitle);
        }
        
    }
}
