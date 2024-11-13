using DTOs.BannedUser;
using DTOs.User;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/BanAPI/[controller]")]
    [ApiController]
    public class BannedUserController(
        [FromKeyedServices("bannedUserService")]IBannedUserService bannedUserService,
        IValidator<BannedUserInsertDTO> bannedUserInsertValidator, IValidator<BannedUserUpdateDTO> bannedUserUpdateValidator
        ) : ControllerBase
    {
        private readonly IValidator<BannedUserInsertDTO> _bannedUserInsertValidator = bannedUserInsertValidator;
        private readonly IValidator<BannedUserUpdateDTO> _bannedUserUpdateValidator = bannedUserUpdateValidator;
        private readonly IBannedUserService _bannedUserService = bannedUserService;

        [HttpGet]
        public async Task<IEnumerable<BannedUserDTO>> Get() => await _bannedUserService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BannedUserDTO>> GetById(int id)
        {
            var result = await _bannedUserService.GetById(id);

            return result is null ? NotFound() : Ok(result);
        }


        [HttpPost("{id}")]
        public async Task<ActionResult<BannedUserDTO>> Add(int id, BanData banData)
        {
            var validationResult = await _bannedUserInsertValidator.ValidateAsync(await _bannedUserService.BanInsertPreview(id, banData));

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var bannedUserDTO = await _bannedUserService.Add(id, banData);

            return CreatedAtAction(nameof(GetById), new { id = bannedUserDTO.Id }, bannedUserDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BannedUserDTO>> Update(int id, BannedUserUpdateDTO bannedUserUpdateDTO)
        {
            var validationResult = await _bannedUserUpdateValidator.ValidateAsync(bannedUserUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
            var bannedUserDTO = await _bannedUserService.Update(id, bannedUserUpdateDTO);

            return bannedUserDTO is null ? NotFound() : Ok(bannedUserDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BannedUserDTO>> Delete(int id)
        {
            var bannedUserDTO = await _bannedUserService.Delete(id);

            return bannedUserDTO is null ? NotFound() : Ok(bannedUserDTO);
        }

    }
}
