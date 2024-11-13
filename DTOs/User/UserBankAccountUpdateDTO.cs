using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class UserBankAccountUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public bool IsMainAccount { get; set; }
        public int UserId { get; set; }
    }
}
