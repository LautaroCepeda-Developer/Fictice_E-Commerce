using DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        List<string> Errors { get; }
        Task<IEnumerable<UserDTO>> Get();
        Task<UserDTO?> GetById(int id);
        Task<int?> GetIdByNickname(string nickname);
        Task<int?> GetIdByEmail(string email);
        Task<bool?> Login(string emailOrNickname, string password);
        Task<UserDTO> Add(UserInsertDTO insertDTO);
        Task<UserDTO?> Update(int id, UserUpdateDTO updateDTO);
        Task<UserDTO?> Delete(int id);
        bool Validate(UserInsertDTO DTO);
        bool Validate(UserUpdateDTO DTO);
    }
}
