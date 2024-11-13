using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ProductSale
{
    public class ProductSaleDTO
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice => PricePerUnit * ProductQuantity;
    }
}
