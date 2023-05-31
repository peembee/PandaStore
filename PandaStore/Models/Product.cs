using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Produkt ID")]
        public int ProductID { get; set; }


        [Required]
        [ForeignKey("Categorys")]
        [DisplayName("Kategori")]
        public int FK_CategoryID { get; set; }
        public virtual Category? Categorys { get; set; }


        [Required]
        [StringLength(50)]
        [DisplayName("Varumärke")]
        public string ProductTitel { get; set; } = string.Empty;


        [Required]
        [StringLength(200)]
        [DisplayName("Beskrivning")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [DisplayName("Specifikation")]
        public string Specification { get; set; } = string.Empty;


        [Required]
        [DisplayName("Pris")]
        public double Price { get; set; }


        [StringLength(80)]
        public string? ImgUrl { get; set; }


        [DisplayName("Lagersaldo")]
        public int InventoryQuantity { get; set; }


        public bool ProductIsActive { get; set; } = true;


        [DisplayName("Nästa leverans")]
        public DateTime NextDelivery { get; set; }

        [NotMapped]
        [DisplayName("Ordinarie pris")]
        public double OriginalPrice { get; set; }
        public virtual ICollection<CustomerRate>? CustomerRates { get; set; }
        public virtual ICollection<Campaign>? Campaigns { get; set; }

    }
}
