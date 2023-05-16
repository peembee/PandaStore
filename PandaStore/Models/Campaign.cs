using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PandaStore.Models
{
    public class Campaign
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaignID { get; set; }


        [Required]
        [ForeignKey("Users")]
        public int FK_UserID { get; set; }
        public virtual User Users { get; set; }


        [Required]
        [ForeignKey("Products")]
        public int FK_ProductID { get; set; }
        public virtual Product Products { get; set; }


        [Required]
        public DateTime StartDate { get; set; }


        [Required]
        public DateTime EndDate { get; set; }


        [Required]
        public double Discount { get; set; }


        [Required]
        public bool IsActive { get; set; }

    }
}
