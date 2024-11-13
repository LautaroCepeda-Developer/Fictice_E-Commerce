using AutoMapper;
using DTOs.Sale;
using Models;

namespace Automappers
{
    public class SaleMappingProfile : Profile
    {
        public SaleMappingProfile() 
        {
            CreateMap<Sale, SaleDTO>();
            CreateMap<SaleDTO, Sale>();
            CreateMap<SaleInsertDTO, Sale>();
        }
    }
}
