using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Models.UsersModels;
using Data.Entities;
using ForumAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<RegisterController > _logger;
        public RegisterController(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ILogger<RegisterController> logger, 
            ApplicationDbContext applicationDbContext,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            dbContext = applicationDbContext;
            _hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserModel user)
        {
            var newUser = new User { Email = user.Email, UserName = user.UserName, RegistrationDate = DateTime.Now };
            try
            {
                var result = await _userManager.CreateAsync(newUser,user.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account: "+ newUser.UserName+" with password");
                    var registeredUser = await _userManager.FindByNameAsync(newUser.UserName);
                    await _userManager.AddToRoleAsync(registeredUser,"user");
                    return Ok(result);
                }
                _logger.LogError(result.Errors.FirstOrDefault().Description);
                return BadRequest(result.Errors);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromForm] UpdateUserModel updateUserModel)
        {
            var userToUpdate = await _userManager.FindByIdAsync(updateUserModel.Id);
            if (userToUpdate != null)
            {

                userToUpdate.AvatarName = await SaveImage(updateUserModel.ImageFile,userToUpdate.UserName);

    
                if (updateUserModel.BirthDate != null)
                    userToUpdate.BirthDate = (DateTime)updateUserModel.BirthDate;
                userToUpdate.FirstName = updateUserModel.FirstName;
                userToUpdate.LastName = updateUserModel.LastName;
                userToUpdate.PhoneNumber = updateUserModel.PhoneNumber;
                if(updateUserModel.Email != null)
                    userToUpdate.Email = updateUserModel.Email;
                try
                {
                    var result = await _userManager.UpdateAsync(userToUpdate);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"User {userToUpdate.UserName} updated profile");
                        return Ok(result.Succeeded);
                    }
                    _logger.LogError(result.Errors.FirstOrDefault().Description);
                    return BadRequest(result.Errors);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile,string username)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ','-');
            imageName = username + imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath,"Images",imageName);
            string rootFolderPath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            string filesToDelete = $"*{username}*";   // Only delete images containing "DeleteMe" in their filenames
            string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete);
            foreach (string file in fileList)
            {
                //System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                System.IO.File.Delete(file);
            }
            using (var fileStream = new FileStream(imagePath,FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
