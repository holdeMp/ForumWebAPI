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
        public SectionController(ISectionService sectionService, UserManager<User> userManager, ILogger<RolesController> logger, IMapper mapper)
        {
            _sectionService = sectionService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;

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
    }
}
