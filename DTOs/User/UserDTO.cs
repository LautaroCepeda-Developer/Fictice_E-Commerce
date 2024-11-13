using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Infractions { get; set; }
        public int PersonalInformationId { get; set; }
        public required UserPersonalInformation UserPersonalInformation { get; set; }
    }
}
