using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProductSale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int ProductQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerUnit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice => PricePerUnit * ProductQuantity;

        // Foreign Key References
        [ForeignKey(nameof(SaleId))]
        public virtual Sale Sale { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
