using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiDemo.Models;

namespace NetCoreWebApiDemo.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/vehicle")]
    [Route("api/v{version:apiVersion}/vehicle")]
    public class VehicleController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult GetV1()
        => Ok(new[] { new { id = 1, plate = "34 MRT 95" } });

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetV2()
            => Ok(new[] { new Vehicle { Id = 1, DriverName = "Mert Özen", Plate = null } });
    }
}
