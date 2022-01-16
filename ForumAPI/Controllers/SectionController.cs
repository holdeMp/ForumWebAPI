using AutoMapper;
using Business.Interfaces;
using Business.Models;
using DAL.Interfaces;
using Data;
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
        private readonly ForumDbContext _forumDbContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
         private readonly ILogger<SectionController> _logger;
        private readonly ISectionTitleService _SectionTitleService;
        public SectionController(ISectionService sectionService, 
            ForumDbContext forumDbContext, 
            IUnitOfWork unitOfWork,
            ISectionTitleService SectionTitleService, UserManager<User> userManager, 
            ILogger<SectionController> logger, IMapper mapper)
        {
            _sectionService = sectionService;
            _forumDbContext = forumDbContext;
            _mapper = mapper;
            _logger = logger;
            _SectionTitleService = SectionTitleService;
            _unitOfWork = unitOfWork;
        }
        //POST: /section/
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] AddSectionModel addSectionModel)
        {
            var SectionTitle = _SectionTitleService.FindByName(addSectionModel.SectionTitle).Result;

            if (SectionTitle==null)
            {
                _logger.LogError("Incorrect section title name");
                return BadRequest("Incorrect section title name");
            }
            
            try {
                
                SectionModel sectionModel = new SectionModel {
                    
                    Name = addSectionModel.Name,SectionTitleId=SectionTitle.Id,
                   
                };

                Section sectionn = _mapper.Map<SectionModel, Section>(sectionModel);
                _forumDbContext.ChangeTracker.Clear();
                await _sectionService.AddAsync(sectionModel);
            }
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
        public ActionResult<IEnumerable<SectionModel>> GetSections()
        {
            var sections = _sectionService.GetAll();
            return Ok(sections);
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SectionModel>> GetSectionBySectionTitleId(int id)
        {
            var section = _sectionService.GetAll().Where(i => i.SectionTitleId == id);
            return Ok(section);
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateSection([FromBody] UpdateSectionModel updateSectionModel)
        {

            if (updateSectionModel.Name == "" || updateSectionModel.Name.Length < 3)
            {
                _logger.LogError("Incorrect section name");
                return BadRequest("Incorrect section name");
            }
            try {
                var section = _sectionService.GetByIdAsync(updateSectionModel.SectionId).Result;
                section.Name = updateSectionModel.Name;
                var sectionTitle = _SectionTitleService.FindByName(updateSectionModel.SectionTitle);
                section.SectionTitleId = sectionTitle.Result.Id;
                _forumDbContext.ChangeTracker.Clear();
                await _sectionService.UpdateAsync(section);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            

            return Ok(_sectionService.GetByIdAsync(updateSectionModel.SectionId).Result);
        }
    }
}
