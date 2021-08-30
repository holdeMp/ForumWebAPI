﻿using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL;
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
            this.unitOfWork = unitOfWork;
            this.sectionRep = unitOfWork.SectionRepository;
            this.mapper = mapper;
        }
        public async Task AddAsync(SectionModel sectionModel)
        {
            if (sectionModel.Name=="" || GetAll().Any(i=>i.Name==sectionModel.Name))
            {
                throw new ForumException("Incorrect name");
            }
            var section = mapper.Map<SectionModel, Section>(sectionModel);
            await sectionRep.AddAsync(section);
            await unitOfWork.SaveAsync();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SectionModel> GetAll()
        {
            var sections = sectionRep.FindAll();
            var sectionsModels = new List<SectionModel>();
            foreach (var section in sections)
            {
                sectionsModels.Add(mapper.Map<Section, SectionModel>(section));
            }

            return sectionsModels;
        }

        public Task<SectionModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SectionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
