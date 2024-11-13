using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BannedUser
{
    public class BannedUserUpdateDTO
    {
        public string BanMotive { get; set; }
        public DateTime BanDateTimeEnd { get; set; }
    }
}
