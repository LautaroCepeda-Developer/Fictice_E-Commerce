using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SaleValue { get; set; }

        public DateTime DateTime => DateTime.UtcNow;

        public int BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public virtual User Buyer { get; set; }
    }
}
