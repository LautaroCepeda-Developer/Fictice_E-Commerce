using AutoMapper;
using DTOs.CompanyData;
using Models;

namespace Automappers
{
    public class CompanyDataMappingProfile : Profile
    {
        public CompanyDataMappingProfile() 
        {
            CreateMap<CompanyDataDTO, CompanyData>();
            CreateMap<CompanyData, CompanyDataDTO>();
            CreateMap<CompanyDataInsertDTO, CompanyData>();
            CreateMap<CompanyDataUpdateDTO, CompanyData>();
        }
    }
}
