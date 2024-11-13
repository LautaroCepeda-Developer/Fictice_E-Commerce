using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BannedUser
{
    public class BannedBankAccountDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BanMotive { get; set; }
        public DateTime BanDateTimeStart { get; set; } = DateTime.Now;
        public DateTime BanDateTimeEnd { get; set; }
    }
}
