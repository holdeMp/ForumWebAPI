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
                var userByName = await _userManager.FindByNameAsync(user.UserNameOrEmail);
                var userByEmail = await _userManager.FindByEmailAsync(user.UserNameOrEmail);
                if (userByName != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(userByName, user.Password, user.RememberMe, false);
                    if (result.Succeeded)
                    {

                        _logger.LogInformation("User login " + userByName.UserName);
                        return Ok(new ResponsedUserModel
                        {
                            UserName = userByName.UserName,
                            Email = userByName.Email,
                            AvatarPath = userByName.AvatarPath,
                            BirthDate = userByName.BirthDate,
                            RegistrationDate = userByName.RegistrationDate,
                            FirstName = userByName.FirstName,
                            LastName = userByName.LastName,
                            Id = userByName.Id,
                            PhoneNumber = userByName.PhoneNumber,
                            PhoneNumberConfirmed = userByName.PhoneNumberConfirmed,
                            ConfirmedEmail = userByName.PhoneNumberConfirmed
                        });
                    }
                    _logger.LogInformation("Incorrect login attempt " + userByName.UserName);
                    return BadRequest("Incorrect login attempt");
                }
                if (userByEmail != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(userByEmail, user.Password, user.RememberMe, false);
                    if (result.Succeeded)
                    {

                        _logger.LogInformation("User login " + userByEmail.UserName);
                        return Ok(new ResponsedUserModel
                        {
                            UserName = userByEmail.UserName,
                            Email = userByEmail.Email,
                            AvatarPath = userByEmail.AvatarPath,
                            BirthDate = userByEmail.BirthDate,
                            RegistrationDate = userByEmail.RegistrationDate,
                            FirstName = userByEmail.FirstName,
                            LastName = userByEmail.LastName,
                            Id = userByEmail.Id,
                            PhoneNumber = userByEmail.PhoneNumber,
                            PhoneNumberConfirmed = userByEmail.PhoneNumberConfirmed,
                            ConfirmedEmail = userByEmail.PhoneNumberConfirmed
                        });
                    }
                    _logger.LogInformation("Incorrect login attempt " + userByEmail.UserName);
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
