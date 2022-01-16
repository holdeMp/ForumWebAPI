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
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRep;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ThemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _themeRep = unitOfWork.ThemeRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddAsync(ThemeModel model)
        {

            if (model.Name == "   " || model.Name.Length < 3 || GetAll().Any(i => i.Name == model.Name))
            {
                throw new ForumException("Incorrect name");
            }
            var theme = mapper.Map<ThemeModel, Theme>(model);
            await unitOfWork.ThemeRepository.AddAsync(theme);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            var theme = unitOfWork.ThemeRepository.GetByIdAsync(modelId).Result;
            if (theme == null)
            {
                throw new ForumException("Incorrect theme id to delete");
                
            }
            unitOfWork.ThemeRepository.Delete(theme);
            await unitOfWork.SaveAsync();

        }

        public IEnumerable<ThemeModel> GetAll()
        {
            var themes = unitOfWork.ThemeRepository.FindAll();
            var themesModels = mapper.Map<IQueryable<Theme>,ICollection<ThemeModel>>(themes);

            return themesModels;
        }

        public async Task<ThemeModel> GetByIdAsync(int id)
        {
            var theme = await _themeRep.GetByIdAsync(id);
            return mapper.Map<Theme, ThemeModel>(theme);

        }

        public async Task UpdateAsync(ThemeModel model)
        {
            if (model.Name == null)
            {
                throw new ForumException("Incorrect name");
            }
            _themeRep.Update(mapper.Map<ThemeModel, Theme>(model));
            await unitOfWork.SaveAsync();
        }
    }
}
