using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class UserInsertDTO
    {
        public required string Nickname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string NationalIdentification { get; set; }
        public required string FullName { get; set; }
        public required string Address { get; set; }
    }
}
