using AutoMapper;
using DTOs.Category;
using Models;

namespace Automappers
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryInsertDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();
        }
    }
}
