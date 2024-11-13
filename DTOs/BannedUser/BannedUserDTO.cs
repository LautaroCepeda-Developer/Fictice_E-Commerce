using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BannedUser
{
    public class BannedUserDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public int Infractions { get; set; }
        public string BanMotive { get; set; }
        public DateTime BanDateTimeStart => DateTime.UtcNow;
        public DateTime BanDateTimeEnd { get; set; }
        public string NationalIdentification { get; set; }
    }
}
