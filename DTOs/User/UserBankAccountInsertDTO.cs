﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class UserBankAccountInsertDTO
    {
        public required string Name { get; set; }
        public required string AccountNumber { get; set; }
        public required int UserId { get; set; }
    }
}
