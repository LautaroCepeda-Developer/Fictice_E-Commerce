using DTOs.Category;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/ProductAPI/[controller]")]
    [ApiController]
    public class CategoryController(
        ICommonService<CategoryDTO, CategoryInsertDTO, CategoryUpdateDTO> categoryService,
        IValidator<CategoryInsertDTO> categoryInsertValidator, IValidator<CategoryUpdateDTO> categoryUpdateValidator
        ) : ControllerBase
    {
        private readonly ICommonService<CategoryDTO, CategoryInsertDTO, CategoryUpdateDTO> _categoryService = categoryService;
        private readonly IValidator<CategoryInsertDTO> _categoryInsertValidator = categoryInsertValidator;
        private readonly IValidator<CategoryUpdateDTO> _categoryUpdateValidator = categoryUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get() => await _categoryService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var result = await _categoryService.GetById(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Add(CategoryInsertDTO categoryInsertDTO)
        {
            var validationResult = await _categoryInsertValidator.ValidateAsync(categoryInsertDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_categoryService.Validate(categoryInsertDTO)) return BadRequest(_categoryService.Errors);

            var categoryDTO = await _categoryService.Add(categoryInsertDTO);

            return CreatedAtAction(nameof(GetById), new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> Update(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            categoryUpdateDTO.Id = id;

            var validationResult = await _categoryUpdateValidator.ValidateAsync(categoryUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_categoryService.Validate(categoryUpdateDTO)) return BadRequest(_categoryService.Errors);

            var categoryDTO = await _categoryService.Update(id, categoryUpdateDTO);

            return categoryDTO is null ? NotFound() : Ok(categoryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var result = await _categoryService.Delete(id);

            return result is null ? NotFound() : Ok(result);
        }
    }
}
