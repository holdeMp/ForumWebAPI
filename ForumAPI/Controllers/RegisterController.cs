using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using ForumAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<RegisterController > _logger;
        public RegisterController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<RegisterController> logger, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            dbContext = applicationDbContext;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserModel user)
        {
            try
            {
                var newUser = new User { Email = user.Email, UserName = user.UserName, RegistrationDate = DateTime.Now };
                var result = await _userManager.CreateAsync(newUser,user.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return Ok(result);
                }
                _logger.LogInformation(result.Errors.FirstOrDefault().Description);
                return BadRequest(result.Errors);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
