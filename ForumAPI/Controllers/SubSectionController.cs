using AutoMapper;
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

namespace ForumAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubSectionController : ControllerBase
    {
        private readonly ISubSectionService _subSectionService;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;
        public SubSectionController(ISubSectionService subSectionService, 
            ILogger<RolesController> logger, IMapper mapper)
        {
            _subSectionService = subSectionService;            
            _mapper = mapper;
            _logger = logger;
        }
        //POST: /subsection/
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] SubSectionModel subSectionModel)
        {
            try
            {
                await _subSectionService.AddAsync(subSectionModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            var addedSubSection = _subSectionService.GetAll().Last();
            addedSubSection.ThemesIds = null;
            _logger.LogInformation("Added section:" + addedSubSection);
            return Ok(addedSubSection);
        }
    }
}
