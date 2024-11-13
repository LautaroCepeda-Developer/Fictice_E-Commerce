using AutoMapper;
using DTOs.Product;
using Models;

namespace Automappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductInsertDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();
        }
    }
}
