using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Sale
{
    public class SaleCompleteRequestDTO
    {
        public required SaleInsertDTO saleInsertDTO { get; set; }
        public required IEnumerable<Models.ProductInfo> Products { get; set; }
    }
}
