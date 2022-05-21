
using System;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL.Interfaces;
using Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(AnswerModel model)
        {
            var validator = new AnswerModelValidator();
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                throw new FormatException("Incorrect answer");
            }

            var answer = _mapper.Map<AnswerModel, Answer>(model);
            await _answerRepository.AddAsync(answer);
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AnswerModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<AnswerModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(AnswerModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
