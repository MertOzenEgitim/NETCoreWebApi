using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiDemo.Models;

namespace NetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(new UserDto { Age=30,Email="mertozensoru@gmail.com",Name="Mert"});
        }

        [Authorize]
        [HttpGet("secret")]
        public IActionResult Secret()
        {
            return Ok(new UserDto { Age = 30, Email = "mertozensoru@gmail.com", Name = "Mert" });
        }

        [Authorize(Policy = "HasXHeader")]
        [HttpGet("secure")]
        public IActionResult Secure()
        {
            return Ok(new UserDto { Age = 30, Email = "mertozensoru@gmail.com", Name = "Mert" });
        }
    }
}
