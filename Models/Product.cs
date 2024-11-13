using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateOnly PublicationDate => DateOnly.FromDateTime(DateTime.UtcNow);

        public bool IsActive { get; set; } = true;

        public required int CategoryId { get; set; }
        
        public required int SellerId { get; set; }

        // Foreign Key References
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(SellerId))]
        public virtual User User { get; set; }
    }
}
