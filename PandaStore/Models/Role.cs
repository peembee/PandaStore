using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }


        [Required]
        [StringLength(25)]
        public string RoleTitel { get; set; }


        public virtual ICollection<User> Users { get; set; }
    }
}
