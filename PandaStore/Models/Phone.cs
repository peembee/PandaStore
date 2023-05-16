using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PandaStore.Models
{
    public class Phone
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneID { get; set; }


        [Required]
        [ForeignKey("Users")]
        public int FK_UserID { get; set; }
        public virtual User Users { get; set; }


        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
