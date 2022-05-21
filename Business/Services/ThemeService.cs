using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Business.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ThemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _themeRep = unitOfWork.ThemeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(ThemeModel model)
        {

            if (model.Name == "   " || model.Name.Length < 3 || GetAll().Any(i => i.Name == model.Name))
            {
                throw new ForumException("Incorrect name");
            }
            var theme = _mapper.Map<ThemeModel, Theme>(model);
            await _unitOfWork.ThemeRepository.AddAsync(theme);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            var theme = _unitOfWork.ThemeRepository.GetByIdAsync(modelId).Result;
            if (theme == null)
            {
                throw new ForumException("Incorrect theme id to delete");
                
            }
            _unitOfWork.ThemeRepository.Delete(theme);
            await _unitOfWork.SaveAsync();

        }

        public async Task<ThemeModel> FindByName(string themeName)
        {
            var model = await _themeRep.FindByNameAsync(themeName);
            return _mapper.Map<Theme, ThemeModel>(model);
        }

        public IEnumerable<ThemeModel> GetAll()
        {
            var themes = _unitOfWork.ThemeRepository.FindAll();
            var themesModels = _mapper.Map<IQueryable<Theme>,ICollection<ThemeModel>>(themes);

            return themesModels;
        }

        public async Task<ThemeModel> GetByIdAsync(int id)
        {
            var theme = await _themeRep.GetByIdAsync(id);
            return _mapper.Map<Theme, ThemeModel>(theme);
        }

        public async Task UpdateAsync(ThemeModel model)
        {
            if (model.Name == null)
            {
                throw new ForumException("Incorrect name");
            }
            _themeRep.Update(_mapper.Map<ThemeModel, Theme>(model));
            await _unitOfWork.SaveAsync();
        }
    }
}
