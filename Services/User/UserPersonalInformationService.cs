using AutoMapper;
using DTOs.User;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services.User
{
    public class UserPersonalInformationService(IUserPersonalInformationRepository userPersonalInformationRepository, IMapper mapper) : IUserPersonalInformationService
    {
        private readonly IUserPersonalInformationRepository _userPersonalInformationRepository = userPersonalInformationRepository;
        private readonly IMapper _mapper = mapper;

        // Empty error list
        public List<string> Errors { get; } = [];

        // Getting all users
        public async Task<IEnumerable<UserPersonalInformationDTO>> Get()
        {
            var userPI = await _userPersonalInformationRepository.Get();

            return userPI.Select(_mapper.Map<UserPersonalInformationDTO>);
        }

        public async Task<UserPersonalInformationDTO?> GetById(int id)
        {
            // Getting the user
            var userPI = await _userPersonalInformationRepository.GetById(id);

            // Checking if the user exists
            if (userPI is null) return null;

            // Automapping the entity
            var userPIDTO = _mapper.Map<UserPersonalInformationDTO>(userPI);

            // Returning the user
            return userPIDTO;
        }

        public async Task<UserPersonalInformationDTO> Update(int id, UserPersonalInformationUpdateDTO userPersonalInformationUpdateDTO)
        {
            // Getting the user
            var user = await _userPersonalInformationRepository.GetById(id);

            // Checking if the user exists
            if (user is null) return null;

            // Mapping the entity
            user = _mapper.Map(userPersonalInformationUpdateDTO, user);

            // Updating the user and saving the changes
            _userPersonalInformationRepository.Update(user);
            await _userPersonalInformationRepository.Save();

            // Mapping the DTO to be returned
            var userDTO = _mapper.Map<UserPersonalInformationDTO>(user);

            // Returning the updated user
            return userDTO;
        }

        // Validate update DTO
        public bool Validate(UserPersonalInformationUpdateDTO userPersonalInformationUpdateDTO)
        {
            if (!_userPersonalInformationRepository.Search(u => u.NationalIdentification == userPersonalInformationUpdateDTO.NationalIdentification && u.Id != userPersonalInformationUpdateDTO.Id).Any()) return true;

            Errors.Add("The National ID already exists.");
            return false;
        }
    }
}
