using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class BannedBankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string AccountNumber { get; set; }
        public required string BanMotive { get; set; }
        public DateTime BanDateTimeStart => DateTime.UtcNow;
        public required DateTime BanDateTimeEnd { get; set; }
    }
}
