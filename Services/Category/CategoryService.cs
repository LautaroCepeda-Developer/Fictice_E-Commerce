using AutoMapper;
using DTOs.Category;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Category
{
    public class CategoryService(IRepository<Models.Category> repository, IMapper mapper) : ICommonService<CategoryDTO,CategoryInsertDTO,CategoryUpdateDTO>
    {
        private readonly IRepository<Models.Category> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public List<string> Errors { get; } = [];

        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            var categories = await _repository.Get();

            return categories.Select(_mapper.Map<CategoryDTO>);
        }

        public async Task<CategoryDTO?> GetById(int id)
        {
            var category = await _repository.GetById(id);

            if (category is null) return null;

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public async Task<CategoryDTO> Add(CategoryInsertDTO insertDTO)
        {
            var category = _mapper.Map<Models.Category>(insertDTO);

            await _repository.Add(category);
            await _repository.Save();

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public async Task<CategoryDTO?> Update(int id, CategoryUpdateDTO updateDTO)
        {
            var category = await _repository.GetById(id);

            if (category is null) return null;

            category = _mapper.Map(updateDTO, category);

            _repository.Update(category);
            await _repository.Save();

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public async Task<CategoryDTO?> Delete(int id)
        {
            var category = await _repository.GetById(id);

            if (category is null) return null;

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            _repository.Delete(category);

            await _repository.Save();

            return categoryDTO;
        }

        public bool Validate(CategoryInsertDTO DTO)
        {
            if (!_repository.Search(c => c.Name == DTO.Name).Any()) return true;

            Errors.Add("This category already exists.");
            return false;
        }

        public bool Validate(CategoryUpdateDTO DTO)
        {
            if (!_repository.Search(c => c.Name == DTO.Name && c.Id != DTO.Id).Any()) return true;

            Errors.Add("This category already exists.");
            return false;
        }
    }
}
