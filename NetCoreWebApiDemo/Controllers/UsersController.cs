using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreWebApiDemo.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        //// /api/users      
        //public IActionResult GetById()
        //{
        //  return Ok("Mert");
        //}

        //// /api/users/1
        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    if (id == 1)
        //        return Ok("Mert");
        //    return NotFound();
        //}

        //// /api/users/search?role=admin&active=true
        //[HttpGet("search")]
        //public IActionResult Search(string role, bool active)
        //{
        //    return Ok($"Role={role},Active={active}");
        //}

        // /api/users/1
        //[HttpGet("{id}")]
        //public ActionResult<string> GetById(int id)
        //{
        //    if (id == 1)
        //        return Ok("Mert");
        //    return NotFound();
        //}

        //-----------Route Template ile Eşleşme
        //[HttpGet("{id}")]
        //public IActionResult GetById(int id) => Ok($"Kullanıcı {id}");

        //-----------Http Method + Route Kombinasyonu
        // /api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok($"GET {id}");
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string name) => Ok($"PUT {name}");
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => Ok($"DELETE {id}");

        //-------Dinamik Parametreler
        // /api/users/10/mert
        //[HttpGet("{id}/{name}")]
        //public IActionResult GetUserDetail(int id,string name)
        //{
        //    return Ok($"ID={id},Name={name}");
        //}

        //-------Route Constraint
        // /api/users/10
        [HttpGet("{id:int}")]
        public IActionResult GetUserDetail(int id)
        {
            return Ok($"ID={id}");
        }
        // /api/users/username
        [HttpGet("{username:alpha}")]
        public IActionResult GetUserDetail(string username)
        {
            return Ok($"Username={username}");
        }

        //action adını route'a katma
        [HttpGet]
        public IActionResult List() => Ok("Ürünler");
        //sabit route
        //api/users/about
        [HttpGet("about")]
        public IActionResult About() => Ok("Hakkında Bilgisi");

        //opsiyonel parametre
        //api/users
        //api/users/15

        //[HttpGet("{id?}")]
        //public IActionResult GetOptional(int? id)
        //{
        //    return Ok(id.HasValue ? $"{id}" : "ID verilmedi");
        //}

        //varsayılan deger
        //api/users
        //api/users/15

        [HttpGet("{id=1}")]
        public IActionResult GetOptional(int id)
        {
            return Ok($"{id}");
        }

        //varsayılan deger
        //api/users
        //api/users/15

        [HttpGet("details/{id}")]
        [HttpGet("info/{id}")]
        public IActionResult GetDetails(int id)
        {
            return Ok($"{id}");
        }
    }
}
