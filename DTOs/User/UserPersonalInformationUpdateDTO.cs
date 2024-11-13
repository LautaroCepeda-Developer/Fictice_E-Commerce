using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class UserPersonalInformationUpdateDTO
    {
        public int Id { get; set; }
        public string NationalIdentification { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
