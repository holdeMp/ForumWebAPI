using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ThemeService : IThemeService
    {
        public Task AddAsync(ThemeModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ThemeModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ThemeModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ThemeModel model)
        {
            throw new NotImplementedException();
        }
    }
}
