﻿using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Payment
{
    public class PaymentInsertDTO
    {
        public int ProductSaleId { get; set; }
        public int PaymentMethodId { get; set; }
        public string TransactionNumber { get; set; }
        public decimal TransactionValue { get; set; }
        public decimal Tax { get; set; }
        public string SellerBankAccount { get; set; }
    }
}
