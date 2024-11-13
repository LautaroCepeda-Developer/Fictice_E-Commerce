using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DTOs.User
{
    public class UserBankAccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public bool IsMainAccount { get; set; } = false;
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Models.User User { get; set; }
    }
}
