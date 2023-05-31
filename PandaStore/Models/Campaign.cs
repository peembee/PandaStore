using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PandaStore.Models
{
    public class Campaign
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaignID { get; set; }


        [Required]
        [ForeignKey("PandaUsers")]
        public string Id { get; set; } = string.Empty;
        public virtual PandaUser? PandaUsers { get; set; }


        [Required]
        [ForeignKey("Products")]
        public int FK_ProductID { get; set; }
        public virtual Product? Products { get; set; }


        [Required]
        [DisplayName("Startdatum")]
        public DateTime StartDate { get; set; }


        [Required]
        [DisplayName("Slutdatum")]
        public DateTime EndDate { get; set; }


        [Required]
        [DisplayName("Rabatt")]
        public double Discount { get; set; }


        [Required]
        [DisplayName("Aktiv?")]
        public bool IsActive { get; set; }

    }
}
