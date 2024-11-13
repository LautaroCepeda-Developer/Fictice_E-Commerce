using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateOnly PublicationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
    }
}
