using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Services;

namespace NetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService=productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_productService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest();
            }            
        }
        [HttpGet("GetAllFiltered")]
        public IActionResult GetAllFiltered(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sort = null,
            [FromQuery] string? search = null)
        {
            try
            {
                return Ok(_productService.GetPagedFilteredSorted(page, pageSize, sort, search));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product= _productService.GetById(id);
                return product==null?NotFound():Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            try
            {
                _productService.Add(product);
                return CreatedAtAction(nameof(GetById), new {id=product.Id},product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,Product product)
        {
            try
            {
                _productService.Update(id,product);
                return NoContent();
            }
            catch (Exception ex)
            {
                if(ex.Message=="Ürün bulunamadı")
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Ürün bulunamadı")
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }
    }
}
