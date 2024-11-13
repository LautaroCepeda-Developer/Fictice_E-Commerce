using DTOs.User;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/UserAPI/[controller]")]
    [ApiController]
    public class UserController(
        [FromKeyedServices("userService")] IUserService userService,
        IValidator<UserInsertDTO> userInsertValidator, IValidator<UserUpdateDTO> userUpdateValidator
        ) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IValidator<UserInsertDTO> _userInsertValidator = userInsertValidator;
        private readonly IValidator<UserUpdateDTO> _userUpdateValidator = userUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get() => await _userService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var userDTO = await _userService.GetById(id);

            return userDTO is null ? NotFound() : Ok(userDTO);
        }

        [HttpGet("Nickname")]
        public async Task<ActionResult<UserDTO>> GetByNickname([FromHeader] string nickname)
        {
            var userId = await _userService.GetIdByNickname(nickname);

            if (userId is null) return NotFound();

            return await GetById((int)userId);
        }

        [HttpGet("Email")]
        public async Task<ActionResult<UserDTO>> GetByEmail([FromHeader] string email) 
        {
            var userId = await _userService.GetIdByEmail(email);

            if (userId is null) return NotFound();

            return await GetById((int)userId);
        }


        [HttpPost]
        public async Task<ActionResult<UserDTO>> Add(UserInsertDTO userInsertDTO)
        {
            var validationResult = await _userInsertValidator.ValidateAsync(userInsertDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_userService.Validate(userInsertDTO)) return BadRequest(_userService.Errors);

            var userDTO = await _userService.Add(userInsertDTO);

            return CreatedAtAction(nameof(GetById), new { id = userDTO.Id }, userDTO);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromHeader] string nicknameOrEmail, [FromHeader] string password)
        {
            var result = await _userService.Login(nicknameOrEmail, password);

            if (result is null) return NotFound("User not found.");

            if (result is false) return Ok("Incorrect password.");

            return Ok("Loged in.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Update(int id, UserUpdateDTO userUpdateDTO)
        {
            userUpdateDTO.Id = id;

            var validationResult = await _userUpdateValidator.ValidateAsync(userUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_userService.Validate(userUpdateDTO)) return BadRequest(_userService.Errors);

            var userDTO = await _userService.Update(id, userUpdateDTO);

            return userDTO is null ? NotFound() : Ok(userDTO);
        }

        [HttpPut("Nickname")]
        public async Task<ActionResult<UserDTO>> UpdateByNickname([FromHeader]string nickname, UserUpdateDTO userUpdateDTO)
        {
            var userId = await _userService.GetIdByNickname(nickname);

            if (userId is null) return NotFound();

            userUpdateDTO.Id = userId;

            return await Update((int)userId, userUpdateDTO);
        }

        [HttpPut("Email")]
        public async Task<ActionResult<UserDTO>> UpdateByEmail([FromHeader]string email, UserUpdateDTO userUpdateDTO)
        {
            var userId = await _userService.GetIdByEmail(email);

            if (userId is null) return NotFound();

            userUpdateDTO.Id = userId;

            return await Update((int)userId, userUpdateDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> Delete(int id)
        {
            var userDTO = await _userService.Delete(id);

            return userDTO is null ? NotFound() : Ok(userDTO);
        }

        [HttpDelete("Nickname")]
        public async Task<ActionResult<UserDTO>> DeleteByNickname([FromHeader] string nickname) 
        {
            var userid = await _userService.GetIdByNickname(nickname);

            if (userid is null) return NotFound();

            return await Delete((int)userid);
        }

        [HttpDelete("Email")]
        public async Task<ActionResult<UserDTO>> DeleteByEmail([FromHeader] string email) 
        {
            var userid = await _userService.GetIdByEmail(email);

            if (userid is null) return NotFound();

            return await Delete((int)userid);
        }
    }
}
