using DTOs.User;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/UserAPI/[controller]")]
    [ApiController]
    public class UserPersonalInformationController(
        IUserPersonalInformationService userPersonalInformationService,
        IValidator<UserPersonalInformationUpdateDTO> userPersonalInformationUpdateValidator) : ControllerBase
    {
        private readonly IUserPersonalInformationService _userPersonalInformationService = userPersonalInformationService;
        private readonly IValidator<UserPersonalInformationUpdateDTO> _userPersonalInformationUpdateValidator = userPersonalInformationUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<UserPersonalInformationDTO>> Get() => await _userPersonalInformationService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPersonalInformationDTO>> GetById(int id)
        {
            var userPIDTO = await _userPersonalInformationService.GetById(id);

            return userPIDTO is null ? NotFound() : Ok(userPIDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserPersonalInformationDTO>> Update(int id, UserPersonalInformationUpdateDTO userPersonalInformationUpdateDTO) 
        {
            var validationResult = await _userPersonalInformationUpdateValidator.ValidateAsync(userPersonalInformationUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_userPersonalInformationService.Validate(userPersonalInformationUpdateDTO)) return BadRequest(_userPersonalInformationService.Errors);

            var userPIDTO = await _userPersonalInformationService.Update(id, userPersonalInformationUpdateDTO);

            return userPIDTO is null ? NotFound() : Ok(userPIDTO);
        }
    }
}
