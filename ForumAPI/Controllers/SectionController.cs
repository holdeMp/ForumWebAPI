using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;
        private readonly ISectionTitleService _sectionTitleService;
        public SectionController(ISectionService sectionService,ISectionTitleService sectionTitleService, UserManager<User> userManager, ILogger<RolesController> logger, IMapper mapper)
        {
            _sectionService = sectionService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _sectionTitleService = sectionTitleService;

        }
        //POST: /section/
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] SectionModel sectionModel)
        {

            if (sectionModel.Name == "" || sectionModel.Name.Length < 3)
            {
                _logger.LogError("Incorrect section name");
                return BadRequest("Incorrect section name");
            }
            try { await _sectionService.AddAsync(sectionModel); }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            var section = _sectionService.GetAll().Last();

            return Ok(section);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SectionModel>> GetSection()
        {
            var sections = _sectionService.GetAll();
            return Ok(sections);
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateSection([FromBody] UpdateSectionModel updateSectionModel)
        {

            if (updateSectionModel.SectionModel.Name == "" || updateSectionModel.SectionModel.Name.Length < 3)
            {
                _logger.LogError("Incorrect section name");
                return BadRequest("Incorrect section name");
            }
            try { 
                await _sectionService.UpdateAsync(updateSectionModel.SectionModel);
                var sectionTitle = _sectionTitleService.GetAll().FirstOrDefault(i => i.Name == updateSectionModel.SectionTitle);
                sectionTitle.Sections.Add(_mapper.Map<SectionModel, Section>(updateSectionModel.SectionModel));
                await _sectionTitleService.UpdateAsync(sectionTitle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            var section = _sectionService.GetAll().Last();

            return Ok(section);
        }
    }
}
