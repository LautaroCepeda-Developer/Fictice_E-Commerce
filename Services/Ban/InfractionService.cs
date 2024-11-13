using AutoMapper;
using DTOs.User;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services.Ban
{
    public class InfractionService(
        [FromKeyedServices("userService")]IUserService userService,
        [FromKeyedServices("bannedUserService")]IBannedUserService banService,
        [FromKeyedServices("publicUserService")]ICommonPublicService<UserDTO, UserGetPublicDTO> publicUserService,
        IMapper mapper) : IInfractionService
    {
        private readonly IBannedUserService _banService = banService;
        private readonly IUserService _userService = userService;
        private readonly ICommonPublicService<UserDTO, UserGetPublicDTO> _publicUserService = publicUserService;
        private readonly IMapper _mapper = mapper;

        public async Task<int?> GetUserInfractions(int id)
        {
            var user = await _publicUserService.GetPublicById(id);

            if (user is null) return null;

            return user.Infractions;
        }

        public async Task<UserGetPublicDTO?> AddOrRemoveInfractions(int id, int quantity)
        {
            var userDTO = await _userService.GetById(id);
            if (userDTO is null) return null;

            userDTO.Infractions += quantity;

            if (userDTO.Infractions < 0) { userDTO.Infractions = 0; }

            var userModel = _mapper.Map<Models.User>(userDTO);
            var userUpdateDTO = _mapper.Map<UserUpdateDTO>(userModel);

            await _userService.Update(id,userUpdateDTO);

            // If the user exceeds the limit of violations, the user will be banned
            if (userDTO.Infractions >= 5) 
            {
                var permaDate = DateTime.UtcNow.AddYears(999);

                var banData = new BanData { BanMotive = "The maximum number of infractions has been exceeded.", BanDateTimeEnd = permaDate };

                await _banService.Add(id, banData);
            }
            return _mapper.Map<UserGetPublicDTO>(userModel);
        }
    }
}
