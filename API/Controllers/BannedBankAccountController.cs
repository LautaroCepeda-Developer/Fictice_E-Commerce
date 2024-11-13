using DTOs.BannedUser;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/BanAPI/[controller]")]
    [ApiController]
    public class BannedBankAccountController(
        [FromKeyedServices("bannedBankAccountService")]IBannedBankAccountService bannedBankAccountService,
        IValidator<BannedBankAccountInsertDTO> bannedBankAccountInsertValidator, IValidator<BannedBankAccountUpdateDTO> bannedBankAccountUpdateValidator
        ) : ControllerBase
    {
        private readonly IBannedBankAccountService _bannedBankAccountService = bannedBankAccountService;
        private readonly IValidator<BannedBankAccountInsertDTO> _bannedBankAccountInsertValidator = bannedBankAccountInsertValidator;
        private readonly IValidator<BannedBankAccountUpdateDTO> _bannedBankAccountUpdateValidator = bannedBankAccountUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<BannedBankAccountDTO>> Get() => await _bannedBankAccountService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BannedBankAccountDTO>> GetById(int id)
        {
            var result = await _bannedBankAccountService.GetById(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<BannedBankAccountDTO>> Add(int id, BanData banData)
        {
            var validationResult = await _bannedBankAccountInsertValidator.ValidateAsync(await _bannedBankAccountService.BanInsertPreview(id, banData));

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var bannedBankAccountDTO = await _bannedBankAccountService.Add(id, banData);

            return CreatedAtAction(nameof(GetById), new { id = bannedBankAccountDTO.Id }, bannedBankAccountDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BannedBankAccountDTO>> Update(int id, BannedBankAccountUpdateDTO bannedBankAccountUpdateDTO)
        {
            var validationResult = await _bannedBankAccountUpdateValidator.ValidateAsync(bannedBankAccountUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var bannedBankAccountDTO = await _bannedBankAccountService.Update(id, bannedBankAccountUpdateDTO);

            return bannedBankAccountDTO is null ? NotFound() : Ok(bannedBankAccountDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BannedBankAccountDTO>> Delete(int id)
        {
            var bannedBankAccountDTO = await _bannedBankAccountService.Delete(id);

            return bannedBankAccountDTO is null ? NotFound() : Ok(bannedBankAccountDTO);
        }
    }
}
