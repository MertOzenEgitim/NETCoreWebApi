using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Models.Product;

namespace NetCoreWebApiDemo.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();

        ProductDto? GetById(int id);
        void Add(ProductSaveDto product);
        void Update(int id,ProductSaveDto product);
        void Delete(int id);
        Result<Product> GetPagedFilteredSorted(int page, int pageSize, string? sort, string? search);
    }
}
