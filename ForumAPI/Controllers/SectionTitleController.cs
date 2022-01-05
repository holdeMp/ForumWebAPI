using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SectionTitleController : ControllerBase
    {
        private readonly ISectionTitleService _sectionTitleService;
        private readonly UserManager<User> _userManager;
        private readonly ForumDbContext _forumDbContext;
        private readonly IMapper _mapper;
        private readonly ISectionService _sectionService;
        private readonly ILogger<SectionTitleController> _logger;
        public SectionTitleController(ISectionTitleService sectionTitleService, 
            ISectionService sectionService, 
            UserManager<User> userManager, 
            ILogger<SectionTitleController> logger, IMapper mapper,
            ForumDbContext forumDbContext)
        {
            _sectionService = sectionService;
            _sectionTitleService = sectionTitleService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _forumDbContext = forumDbContext;
        }
        //POST: /section/
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] SectionTitleModel SectionTitleModel)
        {

            if (SectionTitleModel.Name == "" || SectionTitleModel.Name.Length < 3)
            {
                _logger.LogError("Incorrect section name");
                return BadRequest("Incorrect section name");
            }
            try { await _sectionTitleService.AddAsync(SectionTitleModel); }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            var SectionTitle = _sectionTitleService.GetAll().Last();

            return Ok(SectionTitle);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SectionModel>> GetSection()
        {
            var sectionsTitles = _sectionTitleService.GetAll();
            return Ok(sectionsTitles);
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateSection([FromBody] SectionTitleModel SectionTitleModel)
        {

            if (SectionTitleModel.Name == "" || SectionTitleModel.Name.Length < 3)
            {
                _logger.LogError("Incorrect section name");
                return BadRequest("Incorrect section name");
            }
            try
            {
                
                foreach (var section in SectionTitleModel.Sections)
                {
                    var sectionn = _sectionService.FindByName(section.Name).Result;
                    sectionn.SectionTitleId = SectionTitleModel.Id;
                    _forumDbContext.ChangeTracker.Clear();
                    await _sectionService.UpdateAsync(sectionn);
                }
                SectionTitleModel.Sections = null;
                await _sectionTitleService.UpdateAsync(SectionTitleModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            var updatedSectionTitle = await _sectionTitleService.GetByIdAsync(SectionTitleModel.Id);
            updatedSectionTitle.Sections = null;
            return Ok(updatedSectionTitle);
        }
    }
}
