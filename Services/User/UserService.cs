using AutoMapper;
using DTOs.User;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

using Models;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services.User
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _repository = userRepository;
        private readonly IMapper _mapper = mapper;

        // Empty error list
        public List<string> Errors { get; } = [];

        public async Task<IEnumerable<UserDTO>> Get()
        {
            // Getting all users
            var users = await _repository.Get();

            // Returning the list of all users mapped to DTO
            return users.Select(_mapper.Map<UserDTO>);
        }

        public async Task<UserDTO?> GetById(int id)
        {
            // Getting the user
            var user = await _repository.GetById(id);

            // Checking if the user exists
            if (user is null) return null;

            // Automapping the entity
            var userDTO = _mapper.Map<UserDTO>(user);

            // Returning the user
            return userDTO;
        }

        public async Task<int?> GetIdByNickname(string nickname)
        {
            var user = await _repository.GetByNickname(nickname);

            if (user is null) return null;

            return user.Id;
        }

        public async Task<int?> GetIdByEmail(string email) 
        {
            var user = await _repository.GetByEmail(email);

            if (user is null) return null;

            return user.Id;
        }

        public async Task<UserDTO> Add(UserInsertDTO userInsertDTO)
        {
            // Automapping the DTO to the User
            var user = _mapper.Map<Models.User>(userInsertDTO);

            user.Password = PasswordHasher.HashPassword(userInsertDTO.Password);

            // Adding the user to the DB and saving the changes
            await _repository.Add(user);
            await _repository.Save();

            // Mapping the DTO to be returned
            var userDTO = _mapper.Map<UserDTO>(user);

            // Returning the added user
            return userDTO;
        }

        public async Task<UserDTO?> Update(int id, UserUpdateDTO userUpdateDTO)
        {
            // Getting the user
            var user = await _repository.GetById(id);

            // Checking if the user exists
            if (user is null) return null;

            user.Password = PasswordHasher.HashPassword(userUpdateDTO.Password);

            // Mapping the entity
            user = _mapper.Map(userUpdateDTO, user);

            // Updating the user and saving the changes
            _repository.Update(user);
            await _repository.Save();

            // Mapping the DTO to be returned
            var userDTO = _mapper.Map<UserDTO>(user);

            // Returning the updated user
            return userDTO;
        }

        public async Task<UserDTO?> Delete(int id)
        {
            // Getting the user
            var user = await _repository.GetById(id);

            // Checking if the user exists
            if (user is null) return null;

            // Automapping the userDTO
            var userDTO = _mapper.Map<UserDTO>(user);

            // Deleting the user and saving the changes
            _repository.Delete(user);

            await _repository.Save();

            // Returning the deleted user
            return userDTO;
        }

        // Simulates a login for testing VerifyPassword of PasswordHasher class
        public async Task<bool?> Login(string emailOrNickname, string password)
        {
            int? userid = emailOrNickname.Contains('@') ? await GetIdByEmail(emailOrNickname) : await GetIdByNickname(emailOrNickname);

            if (userid is null) return null;

            var user = await GetById((int)userid);

            if (user is null) return null;

            return PasswordHasher.VerifyPassword(password, user.Password);
        }

        // Validates the insert DTO
        public bool Validate(UserInsertDTO DTO)
        {
            if (_repository.Search(u => u.Nickname == DTO.Nickname).Any()) { Errors.Add("This username is already in use."); }

            if (_repository.Search(u => u.Email == DTO.Email).Any()) { Errors.Add("This email is already registered in the system."); }

            if (_repository.Search(u=> u.PersonalInformation.NationalIdentification == DTO.NationalIdentification).Any()) { Errors.Add("The National ID already exists."); }

            if (Errors.Count > 0) { return false; }

            return true;
        }

        // Validates the update DTO
        public bool Validate(UserUpdateDTO DTO)
        {
            if (_repository.Search(u => u.Nickname == DTO.Nickname && u.Id != DTO.Id).Any()) { Errors.Add("This username is already in use."); }

            if (_repository.Search(u => u.Email == DTO.Email && u.Id != DTO.Id).Any()) { Errors.Add("This email is already in use."); }

            if (Errors.Count > 0) { return false; }

            return true;
        }
    }
}
