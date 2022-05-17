
using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AnswerService : IAnswerService
    {
        public Task AddAsync(AnswerModel model)
        {
            throw new System.NotImplementedException();
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
