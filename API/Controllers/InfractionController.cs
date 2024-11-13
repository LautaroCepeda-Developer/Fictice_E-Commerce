using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/BanAPI/[controller]")]
    [ApiController]
    public class InfractionController(IInfractionService infractionService) : ControllerBase
    {
        private readonly IInfractionService _infractionService = infractionService;

        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetUserInfractions(int id) 
        { 
            var result = await _infractionService.GetUserInfractions(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DTOs.User.UserGetPublicDTO>> AddOrRemoveInfractions(int id, int quantity) 
        {
            var userDTO = await _infractionService.AddOrRemoveInfractions(id, quantity);

            return userDTO is null ? NotFound() : Ok(userDTO);
        }
    }
}
