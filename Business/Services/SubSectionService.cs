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
    public class SubSectionService:ISubSectionService
    {
        private readonly ISubSectionRepository _subSectionRep;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SubSectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._subSectionRep = unitOfWork.SubSectionRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddAsync(SubSectionModel subSectionModel)
        {
            if (subSectionModel.Name=="   " || subSectionModel.Name.Length < 3 || GetAll().Any(i => i.Name == subSectionModel.Name))
            {
                throw new ForumException("Incorrect name");
            }
            var subSection = mapper.Map<SubSectionModel, SubSection>(subSectionModel);
            await unitOfWork.SubSectionRepository.AddAsync(subSection);
            await unitOfWork.SaveAsync();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubSectionModel> GetAll()
        {
            var subSections = unitOfWork.SubSectionRepository.FindAll();
            var subSectionsModels = new List<SubSectionModel>();
            foreach (var subSection in subSections)
            {
                subSectionsModels.Add(mapper.Map<SubSection, SubSectionModel>(subSection));
            }

            return subSectionsModels;
        }

        async Task<SubSectionModel> ICrud<SubSectionModel>.GetByIdAsync(int id)
        {
            var subSection = await _subSectionRep.GetByIdAsync(id);
            return mapper.Map<SubSection, SubSectionModel>(subSection);
        }

        public async Task UpdateAsync(SubSectionModel model)
        {
            if (model.Name == null)
            {
                throw new ForumException("Incorrect name");
            }
            _subSectionRep.Update(mapper.Map<SubSectionModel, SubSection>(model));
            await unitOfWork.SaveAsync();
        }
    }
}
