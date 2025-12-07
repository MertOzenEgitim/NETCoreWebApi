using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Services;

namespace NetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        public AuthController(JwtService jwtService)
        {
            _jwtService=jwtService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            if(loginRequest.UserName=="mert" && loginRequest.Password == "12345")
            {
                var token = _jwtService.GenerateToken("1", loginRequest.UserName);
                return Ok(new {token});
            }

            return Unauthorized("Kullanıcı adı veya şifre hatalı"); 
        }
    }
}
