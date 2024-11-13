using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class CompanyData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } = 1;

        public required string CompanyName { get; set; }

        public required string CompanyLegalAddres { get; set; }

        public required string EmpressBankAccount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; } // The tax applied to all products selled in the platform
    }
}
