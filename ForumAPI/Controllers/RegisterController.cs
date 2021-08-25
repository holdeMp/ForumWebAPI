using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using ForumAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public RegisterController(UserManager<User> userManager, SignInManager<User> signInManager,ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
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
                    // install cookie
                    await _signInManager.SignInAsync(newUser, false);
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
