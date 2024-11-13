using DTOs.User;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/PublicUserAPI/[controller]")]
    [ApiController]
    public class PublicUserController(
        [FromKeyedServices("publicUserService")] ICommonPublicService<UserDTO, UserGetPublicDTO> publicUserService
        ) : ControllerBase
    {
        private readonly ICommonPublicService<UserDTO, UserGetPublicDTO> _publicUserService = publicUserService;

        [HttpGet]
        public async Task<IEnumerable<UserGetPublicDTO>> GetPublic() => await _publicUserService.GetPublic();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetPublicDTO>> GetPublicById(int id)
        {
            var userPublicDTO = await _publicUserService.GetPublicById(id);

            return userPublicDTO is null ? NotFound() : Ok(userPublicDTO);
        }
    }
}
