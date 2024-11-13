using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class UserGetPublicDTO
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public int Infractions { get; set; }
    }
}
