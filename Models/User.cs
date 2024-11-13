using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Nickname { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public int Infractions { get; set; } = 0;

        public int PersonalInformationId { get; set; }
        
        // Has 'Required' to force the cascade delete
        [ForeignKey(nameof(PersonalInformationId)), DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual UserPersonalInformation PersonalInformation { get; set;}
    }
}
