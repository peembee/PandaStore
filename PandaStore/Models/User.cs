using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class User
    {
        // table - Database
        public string FullName => $"ID #{UserID} | {FirstName} {LastName}";


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }


        [Required]
        [ForeignKey("Roles")]
        public int FK_RoleID { get; set; }
        public virtual Role Roles { get; set; }


        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;



        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;



        [Required]
        [StringLength(70)]
        public string Email { get; set; } = string.Empty;



        [Required]
        public DateTime RegistreredDate { get; set; }



        [Required]
        [StringLength(70)]
        public string Adress { get; set; } = string.Empty;



        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;


        [Required]
        [StringLength(5)]
        public string ZipCode { get; set; } = string.Empty;


        [Required]
        [StringLength(25)]
        public string Password { get; set; } = string.Empty;


        public virtual ICollection<CustomerProduct> CustomerProducts { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
