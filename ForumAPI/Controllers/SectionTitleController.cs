using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ISectionTitleService _SectionTitleService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<SectionTitleController> _logger;
        public SectionTitleController(ISectionTitleService sectionService, UserManager<User> userManager, ILogger<SectionTitleController> logger, IMapper mapper)
        {
            _SectionTitleService = sectionService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;

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
            try { await _SectionTitleService.AddAsync(SectionTitleModel); }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            var SectionTitle = _SectionTitleService.GetAll().Last();

            return Ok(SectionTitle);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SectionModel>> GetSection()
        {
            var sectionsTitles = _SectionTitleService.GetAll();
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
                await _SectionTitleService.UpdateAsync(SectionTitleModel);
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

           
            return Ok(await _SectionTitleService.GetByIdAsync(SectionTitleModel.Id));
        }
    }
}
