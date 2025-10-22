using AutoMapper;
using NetCoreWebApiDemo.Models.Product;

namespace NetCoreWebApiDemo.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductSaveDto, Product>();
        }
    }
}
