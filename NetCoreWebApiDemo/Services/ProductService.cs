using Microsoft.EntityFrameworkCore;
using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Repository;
using System.Globalization;

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
        public Result<Product> GetPagedFilteredSorted(int page, int pageSize,string? sort,string? search)
        {
            var query = _repository.GetAllQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }
            
            query = ApplySorting(query, sort);

            var totalCount=query.Count();
            var data = query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

            return new Result<Product>(data,totalCount,page,pageSize );

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
        private IQueryable<Product> ApplySorting(IQueryable<Product> query,string? sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return query.OrderBy(p => p.Id);
            }

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            sort = textInfo.ToTitleCase(sort);

            bool descending = sort.StartsWith("-");
            string property=descending? sort.Substring(1) : sort;

            return descending
                ? query.OrderByDescending(p =>EF.Property<object>(p,property)) 
                : query.OrderBy(p=> EF.Property<object>(p, property));
        }
    }
}
