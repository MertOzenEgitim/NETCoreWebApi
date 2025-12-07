using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiDemo.Exceptions;
using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Models.Product;
using NetCoreWebApiDemo.Services;
using System.Security.Claims;

namespace NetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        /// <summary>
        /// Tüm ürünleri listeler
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        [ProducesResponseType(typeof(NoData), 400)]
        public IActionResult GetAll()
        {
            try
            {
                var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName=User.FindFirstValue("name");
                _logger.LogInformation("GetAll fetched at {time}", DateTime.Now);
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
        /// <summary>
        /// Belirli bir ürünü döner.
        /// </summary>
        /// <param name="id">Ürün benzersiz numarası</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {            
            if(id<=0)
            throw new ArgumentException("Geçersiz ID!");

            if (id == 1)
                throw new NotFoundException("");

            var product= _productService.GetById(id);
            return product==null?NotFound():Ok(product);           
        }

        [HttpPost]
        public IActionResult Add(ProductSaveDto product)
        {
            try
            {
                _productService.Add(product);
                return CreatedAtAction(nameof(GetById),product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,ProductSaveDto product)
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
