using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Models.Product;
using NetCoreWebApiDemo.Repository;
using System.Globalization;

namespace NetCoreWebApiDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public ProductService(IGenericRepository<Product> repository, IMapper mapper, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        public void Add(ProductSaveDto product)
        {
            //Product productEntity = new Product
            //{
            //    Name = product.Name,
            //    Price = product.Price,
            //    Stock = product.Stock
            //};

            var productEntity = _mapper.Map<Product>(product);

            _repository.Add(productEntity);
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

        public IEnumerable<ProductDto> GetAll()
        {

            IEnumerable<Product> products = _repository.GetAll();
            //List<ProductDto> productList = new List<ProductDto>();

            //foreach (var item in products)
            //{
            //    productList.Add(new ProductDto
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Stock = item.Stock,
            //        Price = item.Price
            //    });
            //}

            var productList= _mapper.Map<List<ProductDto>>(products);

            return productList;
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

        public ProductDto? GetById(int id)
        {
            var key = $"product:{id}";

            var entity= _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                entry.SlidingExpiration = TimeSpan.FromMinutes(1);
                entry.Priority = CacheItemPriority.High;
                return _repository.GetById(id);
            });


            var dto= _mapper.Map<ProductDto>(entity);

            return dto;
        }

        public void Update(int id, ProductSaveDto product)
        {
            var key = $"product:{id}";
            var currentProduct = _repository.GetById(id);
            if (currentProduct == null)
                throw new Exception("Ürün bulunamadı");

            currentProduct.Name= product.Name;
            currentProduct.Price= product.Price;
            currentProduct.Stock=product.Stock;

            var productEntity = _mapper.Map<Product>(currentProduct);

            _repository.Update(productEntity);
            _repository.Save();
            _cache.Remove(key);
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
