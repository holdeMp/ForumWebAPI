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
        private readonly ISectionTitleService _sectionTitleService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<SectionTitleController> _logger;
        public SectionTitleController(ISectionTitleService sectionService, UserManager<User> userManager, ILogger<SectionTitleController> logger, IMapper mapper)
        {
            _sectionTitleService = sectionService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;

        }
        //POST: /section/
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] SectionTitleModel sectionTitleModel)
        {

            if (sectionTitleModel.Name == "" || sectionTitleModel.Name.Length < 3)
            {
                _logger.LogError("Incorrect section name");
                return BadRequest("Incorrect section name");
            }
            try { await _sectionTitleService.AddAsync(sectionTitleModel); }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            var sectionTitle = _sectionTitleService.GetAll().Last();

            return Ok(sectionTitle);
        }
    }
}
