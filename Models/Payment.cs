using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required int ProductSaleId { get; set; }
        public required int PaymentMethodId { get; set; }
        public required string TransactionNumber { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public required decimal TransactionValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Tax { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TransactionValueWithTax => TransactionValue - (TransactionValue * Tax / 100);
        
        public required string SellerBankAccount { get; set; }

        // Foreign Key References
        [ForeignKey(nameof(PaymentMethodId))]
        public virtual PaymentMethod PaymentMethod { get; set; }

        [ForeignKey(nameof(ProductSaleId))]
        public virtual ProductSale ProductSale { get; set; }
    }
}
