using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImagesController(UserManager<User> userManager, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            string rootFolderPath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            var image = System.IO.File.OpenRead($"{rootFolderPath}//{user.AvatarName}");
            return File(image,"image/"+Path.GetExtension(user.AvatarName).Replace(".", ""));
        }
    }

}
