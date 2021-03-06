using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

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
        //POST: /sectiontitle/
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] SectionTitleModel SectionTitleModel)
        {

            try { await _sectionTitleService.AddAsync(SectionTitleModel); }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            

            return Ok(SectionTitleModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SectionModel>> GetSectionTitle()
        {
            var sectionsTitles = _sectionTitleService.GetAll();
            return Ok(sectionsTitles);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateSectioTitle([FromBody] SectionTitleModel SectionTitleModel)
        {

            if (SectionTitleModel.Name.Length < 3)
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
            return Ok(updatedSectionTitle);
        }
    }
}
