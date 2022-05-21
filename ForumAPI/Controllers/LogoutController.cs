using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace ForumAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class LogoutController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginController> _logger;
        public LogoutController(SignInManager<User> signManager, ILogger<LoginController> logger)
        {
            _signInManager = signManager;
            _logger = logger;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok();
        }
    }
}
