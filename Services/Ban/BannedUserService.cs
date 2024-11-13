using AutoMapper;
using DTOs.BannedUser;
using Models;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Ban
{
    public class BannedUserService(IRepository<BannedUser> banRepository, IUserRepository userRepository, IMapper mapper) : IBannedUserService
    {
        private readonly IRepository<BannedUser> _banRepository = banRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BannedUserDTO>> Get()
        {
            var bannedUsers = await _banRepository.Get();

            return bannedUsers.Select(_mapper.Map<BannedUserDTO>);
        }

        public async Task<BannedUserDTO?> GetById(int id)
        {
            var bannedUser = await _banRepository.GetById(id);

            if (bannedUser is null) return null;

            var bannedUserDTO = _mapper.Map<BannedUserDTO>(bannedUser);

            return bannedUserDTO;
        }

        // Returns a preview of the ban to be able to validate the insert DTO
        public async Task<BannedUserInsertDTO> BanInsertPreview(int id, BanData banData) 
        {
            var user = await _userRepository.GetById(id);

            if (user is null) return null;

            var bannedUser = _mapper.Map<BannedUser>(user);
            bannedUser.BanMotive = banData.BanMotive;
            bannedUser.BanDateTimeEnd = banData.BanDateTimeEnd;

            return _mapper.Map<BannedUserInsertDTO>(bannedUser);
        }

        public async Task<BannedUserDTO> Add(int id, BanData banData)
        {
            var user = await _userRepository.GetById(id);

            if (user is null) return null;

            var bannedUser = _mapper.Map<BannedUser>(user);
            bannedUser.BanMotive = banData.BanMotive;
            bannedUser.BanDateTimeEnd = banData.BanDateTimeEnd;

            // Add the user to the ban
            await _banRepository.Add(bannedUser);
            // Deleting the user
            _userRepository.Delete(user);
            await _banRepository.Save();

            var bannedUserDTO = _mapper.Map<BannedUserDTO>(bannedUser);

            return bannedUserDTO;
        }

        public async Task<BannedUserDTO?> Update(int id, BannedUserUpdateDTO updateDTO)
        {
            var bannedUser = await _banRepository.GetById(id);

            if (bannedUser is null) return null;

            bannedUser = _mapper.Map(updateDTO, bannedUser);

            _banRepository.Update(bannedUser);
            await _banRepository.Save();

            var bannedUserDTO = _mapper.Map<BannedUserDTO>(bannedUser);

            return bannedUserDTO;
        }

        public async Task<BannedUserDTO?> Delete(int id)
        {
            var bannedUser = await _banRepository.GetById(id);

            if (bannedUser is null) return null;

            var bannedUserDTO = _mapper.Map<BannedUserDTO>(bannedUser);

            _banRepository.Delete(bannedUser);

            await _banRepository.Save();

            return bannedUserDTO;
        }
    }
}
