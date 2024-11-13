using DTOs.CompanyData;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/CompanyAPI/[controller]")]
    [ApiController]
    public class CompanyDataController(
        ICompanyDataService companyDataService,
        IValidator<CompanyDataInsertDTO> companyDataInsertValidator, IValidator<CompanyDataUpdateDTO> companyDataUpdateValidator
        ) : ControllerBase
    {
        private readonly ICompanyDataService _companyDataService = companyDataService;
        private readonly IValidator<CompanyDataInsertDTO> _companyDataInsertValidator = companyDataInsertValidator;
        private readonly IValidator<CompanyDataUpdateDTO> _companyDataUpdateValidator = companyDataUpdateValidator;

        [HttpGet]
        public async Task<ActionResult<CompanyDataDTO>> Get()
        {
            var result = await _companyDataService.Get();

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("Tax")]
        public async Task<ActionResult<decimal>> GetTax()
        {
            var result = await _companyDataService.GetTax();

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDataDTO>> Add(CompanyDataInsertDTO companyDataInsertDTO)
        {
            var validationResult = await _companyDataInsertValidator.ValidateAsync(companyDataInsertDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_companyDataService.Validate(companyDataInsertDTO)) return BadRequest(_companyDataService.Errors);

            var companyDataDTO = await _companyDataService.Add(companyDataInsertDTO);

            return CreatedAtAction(nameof(Get), companyDataDTO);
        }

        [HttpPut]
        public async Task<ActionResult<CompanyDataDTO>> Update(CompanyDataUpdateDTO companyDataUpdateDTO)
        {
            var validationResult = await _companyDataUpdateValidator.ValidateAsync(companyDataUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var companyDataDTO = await _companyDataService.Update(companyDataUpdateDTO);

            return companyDataDTO is null ? NotFound() : Ok(companyDataDTO);
        }
    }
}
