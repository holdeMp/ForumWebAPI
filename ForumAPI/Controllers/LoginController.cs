using Business.Models;
using Data.Entities;
using ForumAPI.Data;
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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<LoginController> _logger;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            dbContext = applicationDbContext;
        }
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginUserModel user)
        {
            try
            {
                var userByName = _userManager.FindByNameAsync(user.UserNameOrEmail);
                var userByEmail = _userManager.FindByEmailAsync(user.UserNameOrEmail);
                if (userByName != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(userByName.Result, user.Password, user.RememberMe, false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User login.");
                        return Ok(result);
                    }
                    _logger.LogInformation("Incorrect login attempt");
                    return BadRequest("Incorrect login attempt");
                }
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
