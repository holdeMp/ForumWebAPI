
using System;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using System.Linq;

namespace Business.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnswerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _answerRepository = unitOfWork.AnswerRepository;
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
            var result = _answerRepository.FindAll();
            var answerModels = result.Select(x => new AnswerModel(x.Id,x.ThemeId,x.AuthorId,x.ReferenceAnswerId,x.Content));
            return answerModels.ToList();
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
