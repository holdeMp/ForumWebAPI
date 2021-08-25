using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL;
using DAL.Interfaces;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SectionServices : IUserService
    {
        private readonly ISectionRepository userRep;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SectionServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userRep = unitOfWork.SectionRepository;
            this.mapper = mapper;
        }
        public async Task AddAsync(Section model)
        {
            if (model.Name=="")
            {
                throw new ForumException();
            }
            
            await userRep.AddAsync(model);
            await unitOfWork.SaveAsync();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Section> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Section model)
        {
            throw new NotImplementedException();
        }
    }
}
