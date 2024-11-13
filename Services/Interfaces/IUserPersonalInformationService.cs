using DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserPersonalInformationService
    {
        public List<string> Errors { get; }
        public Task<IEnumerable<UserPersonalInformationDTO>> Get();
        public Task<UserPersonalInformationDTO?> GetById(int id);
        public Task<UserPersonalInformationDTO> Update(int id, UserPersonalInformationUpdateDTO userPersonalInformationUpdateDTO);
        public bool Validate(UserPersonalInformationUpdateDTO userPersonalInformationUpdateDTO);
    }
}
