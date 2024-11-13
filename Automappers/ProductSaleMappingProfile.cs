using AutoMapper;
using DTOs.ProductSale;
using Models;

namespace Automappers
{
    public class ProductSaleMappingProfile : Profile
    {
        public ProductSaleMappingProfile() 
        {
            CreateMap<ProductSale, ProductSaleDTO>();
            CreateMap<ProductSaleDTO, ProductSale>();
            CreateMap<ProductSaleInsertDTO, ProductSale>();
            
        }
    }
}
