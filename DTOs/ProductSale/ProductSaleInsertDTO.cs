using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ProductSale
{
    public class ProductSaleInsertDTO
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
