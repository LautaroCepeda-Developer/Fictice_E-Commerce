using DTOs.User;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Validators;
using FluentValidation;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/UserAPI/[controller]")]
    [ApiController]
    public class UserBankAccountController(
        [FromKeyedServices("userBankAccountService")] ICommonService<UserBankAccountDTO, UserBankAccountInsertDTO, UserBankAccountUpdateDTO> service,
        IValidator<UserBankAccountInsertDTO> userBankAccountInsertValidator, IValidator<UserBankAccountUpdateDTO> userBankAccountUpdateValidator) : ControllerBase
    {
        private readonly ICommonService<UserBankAccountDTO, UserBankAccountInsertDTO, UserBankAccountUpdateDTO> _service = service;
        private readonly IValidator<UserBankAccountInsertDTO> _userBankAccountInsertValidator = userBankAccountInsertValidator;
        private readonly IValidator<UserBankAccountUpdateDTO> _userBankAccountUpdateValidator = userBankAccountUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<UserBankAccountDTO>> Get() => await _service.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserBankAccountDTO>> GetById(int id)
        {
            var bankAccountDTO = await _service.GetById(id);

            return bankAccountDTO is null ? NotFound() : Ok(bankAccountDTO);
        }

        [HttpPost]
        public async Task<ActionResult<UserBankAccountDTO>> Add(UserBankAccountInsertDTO userBankAccountDTO)
        {
            var validationResult = await _userBankAccountInsertValidator.ValidateAsync(userBankAccountDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            if (!_service.Validate(userBankAccountDTO)) return BadRequest(_service.Errors);

            var bankAccountDTO = await _service.Add(userBankAccountDTO);

            return CreatedAtAction(nameof(GetById), new { id = bankAccountDTO.Id }, bankAccountDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserBankAccountDTO>> Update(int id, UserBankAccountUpdateDTO userBankAccountUpdateDTO)
        {
            userBankAccountUpdateDTO.Id = id;

            var validationResult = await _userBankAccountUpdateValidator.ValidateAsync(userBankAccountUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_service.Validate(userBankAccountUpdateDTO)) return BadRequest(_service.Errors);

            var bankAccountDTO = await _service.Update(id, userBankAccountUpdateDTO);

            return bankAccountDTO is null ? NotFound() : Ok(bankAccountDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserBankAccountDTO>> Delete(int id)
        {
            var userBankAccountDTO = await _service.Delete(id);

            return userBankAccountDTO is null ? NotFound() : Ok(userBankAccountDTO);
        }
    }
}
