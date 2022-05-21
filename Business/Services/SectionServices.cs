using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL.Interfaces;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository sectionRep;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.sectionRep = unitOfWork.SectionRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddAsync(SectionModel sectionModel)
        {
            if (sectionModel.Name=="   " || sectionModel.Name.Length < 3 || GetAll().Any(i=>i.Name==sectionModel.Name))
            {
                throw new ForumException("Incorrect name");
            }
            var section = mapper.Map<SectionModel, Section>(sectionModel);
            await unitOfWork.SectionRepository.AddAsync(section);
            await unitOfWork.SaveAsync();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SectionModel> GetAll()
        {
            var sections = unitOfWork.SectionRepository.FindAll();
            var sectionsModels = new List<SectionModel>();
            foreach (var section in sections)
            {
                sectionsModels.Add(mapper.Map<Section, SectionModel>(section));
            }

            return sectionsModels;
        }
        public async Task<SectionModel> FindByName(string SectionName)
        {
            var model = await sectionRep.FindByNameAsync(SectionName);
            return mapper.Map<Section, SectionModel>(model);
        }
        async Task<SectionModel> ICrud<SectionModel>.GetByIdAsync(int id)
        {
            var sectionTitle = await sectionRep.GetByIdAsync(id);
            return mapper.Map<Section, SectionModel>(sectionTitle);
        }

        public async Task UpdateAsync(SectionModel model)
        {
            if (model.Name == null)
            {
                throw new ForumException("Incorrect name");
            }
            sectionRep.Update(mapper.Map<SectionModel, Section>(model));
            await unitOfWork.SaveAsync(); 
        }
    }
}
