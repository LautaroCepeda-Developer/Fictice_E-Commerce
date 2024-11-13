using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Sale
{
    public class SaleInsertDTO
    {
        public string OrderNumber { get; set; }
        public decimal SaleValue { get; set; }
        public int BuyerId { get; set; }
    }
}
