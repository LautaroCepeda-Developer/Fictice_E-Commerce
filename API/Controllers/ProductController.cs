using DTOs.Product;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/ProductAPI/[controller]")]
    [ApiController]
    public class ProductController(
        ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO> productService,
        IValidator<ProductInsertDTO> productInsertValidator, IValidator<ProductUpdateDTO> productUpdateValidator
        ) : ControllerBase
    {
        private readonly ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO> _productService = productService;
        private readonly IValidator<ProductInsertDTO> _productInsertValidator = productInsertValidator;
        private readonly IValidator<ProductUpdateDTO> _productUpdateValidator = productUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> Get() => await _productService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var result = await _productService.GetById(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Add(ProductInsertDTO productInsertDTO)
        {
            var validationResult = await _productInsertValidator.ValidateAsync(productInsertDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if(!_productService.Validate(productInsertDTO)) return BadRequest(_productService.Errors);

            var productDTO = await _productService.Add(productInsertDTO);

            return CreatedAtAction(nameof(GetById), new {id = productDTO.Id}, productDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> Update(int id, ProductUpdateDTO productUpdateDTO)
        {
            productUpdateDTO.Id = id;

            var validationResult = await _productUpdateValidator.ValidateAsync(productUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (_productService.Validate(productUpdateDTO)) return BadRequest(_productService.Errors);

            var productDTO = await _productService.Update(id, productUpdateDTO);

            return productDTO is null ? NotFound() : Ok(productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var result = await _productService.Delete(id);

            return result is null ? NotFound() : Ok(result);
        }
    }
}
