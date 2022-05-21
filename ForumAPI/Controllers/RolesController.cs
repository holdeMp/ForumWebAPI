using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace ForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<RolesController> _logger;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ILogger<RolesController> logger, ApplicationDbContext applicationDbContext)
        {
            _roleManager = roleManager;
            _logger = logger;
            dbContext = applicationDbContext;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult> CreateRole([FromBody] string parameterRole)
        {
            try
            {
                var role = new IdentityRole { Name=parameterRole};
                var result = await _roleManager.CreateAsync(role); 
                if (result.Succeeded)
                {
                    _logger.LogInformation("New role created . With a name : "+ role);
                    
                    return Ok(result);
                }
                _logger.LogError(result.Errors.FirstOrDefault().Description);
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("addroletouser")]
        public async Task<ActionResult> AddRoleToUser([FromBody] string parameterRole,string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                var role = await _roleManager.FindByNameAsync(parameterRole);
                if (user != null && role != null)
                {
                    _logger.LogInformation("Added new role " + parameterRole + "to user :" + username);
                    var result = await _userManager.AddToRoleAsync(user, parameterRole);
                    if(result.Succeeded)return Ok(result);
                    return BadRequest(result.Errors);
                }
                _logger.LogError("Incorrect role or username while adding role to user");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
