using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DAL.Entities;

namespace ForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager, ILogger<LoginController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserInfo(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
