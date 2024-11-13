using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BannedUser
{
    public class BannedBankAccountInsertDTO
    {
        public string? AccountNumber { get; set; }
        public string BanMotive { get; set; }
        public DateTime BanDateTimeEnd { get; set; }
    }
}
