using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Sale
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal SaleValue { get; set; }
        public DateTime DateTime => DateTime.UtcNow;
        public int BuyerId { get; set; }
    }
}
