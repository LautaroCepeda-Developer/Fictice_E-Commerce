using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class BannedUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Nickname { get; set; }
        public required string Email { get; set; }
        public required int Infractions { get; set; }
        public required string BanMotive { get; set; }
        public DateTime BanDateTimeStart => DateTime.UtcNow;
        public required DateTime BanDateTimeEnd { get; set; }
        public required string NationalIdentification { get; set; }
    }
}
