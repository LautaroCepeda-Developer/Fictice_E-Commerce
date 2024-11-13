using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BannedUser
{
    public class BannedUserInsertDTO
    {
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public int? Infractions { get; set; }
        public string BanMotive { get; set; }
        public DateTime BanDateTimeEnd { get; set; }
        public string? NationalIdentification { get; set; }
    }
}
