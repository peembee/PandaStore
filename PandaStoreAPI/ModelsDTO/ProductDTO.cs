using System.ComponentModel.DataAnnotations;

namespace PandaStore.Models.DTO
{
    public class ProductDTO
    {
        [Required]
        public int FK_CategoryID { get; set; }


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


        public int InventoryQuantity { get; set; }


        public bool ProductIsActive { get; set; } = true;


        public DateTime NextDelivery { get; set; }
    }
}
