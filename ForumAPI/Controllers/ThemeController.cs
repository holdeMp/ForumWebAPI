﻿using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace ForumAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeService;
        private readonly ILogger<ThemeController> _logger;
        private readonly IMapper _mapper;
        private readonly IAnswerService _answerService;

        public ThemeController(ILogger<ThemeController> logger,IThemeService themeService
             , IMapper mapper, IAnswerService answerService)
        {
            _themeService = themeService;
            _answerService = answerService;
            _logger = logger;
            _mapper = mapper;
        }

        //POST: /theme/
        [HttpPost]
        [Authorize(Roles = "user,admin")]
        public async Task<ActionResult> Add([FromBody] AddThemeModel themeModel)
        {
            var addThemeModel = _mapper.Map<AddThemeModel, ThemeModel>(themeModel);
            try
            {
                await _themeService.AddAsync(addThemeModel);
                var theme = await _themeService.FindByName(themeModel.Name);
                if (theme == null)
                {
                    _logger.LogError($"Theme wasn't added, Theme name:{themeModel.Name}");
                    return BadRequest($"Theme wasn't added, Theme name:{themeModel.Name}");
                }

                var answer = themeModel.Answer;
                answer.ThemeId = theme.Id;
                await _answerService.AddAsync(answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(themeModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ThemeModel>> GetThemes()
        {
            var themes = _themeService.GetAll();
            return Ok(themes);
        }

        [HttpGet("subsection/{id:int}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Theme>> GetThemesBySubSectionId(int id)
        {
            var themesModels = _themeService.GetAll().Where(i => i.SubSectionId == id).ToList();
            var themes = _mapper.Map<ICollection<ThemeModel>, ICollection<Theme>>(themesModels);
            return Ok(themes);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Theme>> GetThemeByThemeId(int id)
        {
            var themeModel = await _themeService.GetByIdAsync(id);
            themeModel.ViewCount++;
            var theme = _mapper.Map<ThemeModel, Theme>(themeModel);
            await _themeService.UpdateAsync(themeModel);
            return Ok(theme);
        }
    }
}
