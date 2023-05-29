using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }


        [Required]
        [ForeignKey("Categorys")]
        public int FK_CategoryID { get; set; }
        public virtual Category? Categorys { get; set; }


        [Required]
        [StringLength(50)]
        public string ProductTitel { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Specification { get; set; } = string.Empty;


        [Required]
        public double Price { get; set; }

      
        public string ImgUrl { get; set; }


        public int InventoryQuantity { get; set; }


        public bool ProductIsActive { get; set; } = true;


        public DateTime NextDelivery { get; set; }


        public virtual ICollection<CustomerRate>? CustomerRates { get; set; }
        public virtual ICollection<Campaign>? Campaigns { get; set; }

    }
}
