using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiDemo.Models;

namespace NetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("create/{companyId}")]
        public IActionResult CreateUser([FromRoute] int companyId, [FromQuery] bool isActive, [FromBody] CreateUserDto createUserDto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            return Ok(new { companyId, isActive, createUserDto });
        }
    }
}
