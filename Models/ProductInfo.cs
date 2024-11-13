using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Models
{
    public class ProductInfo
    {
        public required int Id { get; init; }
        public required int Quantity { get; init; }

        // Def value is zero, because the price is validated in the back-end
        public decimal Price { get; set; } = 0m;
    }
}
