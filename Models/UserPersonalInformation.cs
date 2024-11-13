using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class UserPersonalInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string NationalIdentification { get;set; }
        public required string FullName { get; set; }
        public required string Address { get; set; }
    }
}
