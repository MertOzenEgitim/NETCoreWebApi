using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Repository;

namespace NetCoreWebApiDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        public ProductService(IGenericRepository<Product> repository)
        {
            _repository=repository;
        }

        public void Add(Product product)
        {
            _repository.Add(product);
            _repository.Save();
        }

        public void Delete(int id)
        {
            var currentProduct = _repository.GetById(id);
            if (currentProduct == null)
                throw new Exception("Ürün bulunamadı");

            _repository.Delete(currentProduct);
            _repository.Save();
        }

        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public Product? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(int id, Product product)
        {
            var currentProduct = _repository.GetById(id);
            if (currentProduct == null)
                throw new Exception("Ürün bulunamadı");

            currentProduct.Name= product.Name;
            currentProduct.Price= product.Price;
            currentProduct.Stock=product.Stock;

            _repository.Update(currentProduct);
            _repository.Save();
        }
    }
}
