using NetCoreWebApiDemo.Models;

namespace NetCoreWebApiDemo.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        Product? GetById(int id);
        void Add(Product product);
        void Update(int id,Product product);
        void Delete(int id);
    }
}
