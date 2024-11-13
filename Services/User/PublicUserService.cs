using AutoMapper;
using DTOs.User;
using Models;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.User
{
    public class PublicUserService(IUserRepository userRepository, IMapper mapper) : ICommonPublicService<UserDTO, UserGetPublicDTO>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserGetPublicDTO>> GetPublic()
        {
            // Getting all users
            var users = await _userRepository.Get();

            // Returning the list of all users mapped to DTO
            return users.Select(_mapper.Map<UserGetPublicDTO>);
        }

        public async Task<UserGetPublicDTO?> GetPublicById(int id)
        {
            // Getting the user
            var user = await _userRepository.GetById(id);

            // Checking if the user exists
            if (user is null) return null;

            // Automapping the entity
            var userDTO = _mapper.Map<UserGetPublicDTO>(user);

            // Returning the user
            return userDTO;
        }
    }
}
