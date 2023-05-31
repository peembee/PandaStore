using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PandaStore.Models
{
    public class CustomerProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerProductID { get; set; }


        [Required]
        [ForeignKey("PandaUsers")]
        public string Id { get; set; } = string.Empty;
        public virtual PandaUser? PandaUsers { get; set; }



        [Required]
        [ForeignKey("Products")]
        [DisplayName("Produkt")]
        public int FK_ProductID { get; set; }
        public virtual Product? Products { get; set; }


        [Required]
        [DisplayName("Antal")]
        public int Quantity { get; set; }


        [Required]
        [DisplayName("Pris")]
        public double Price { get; set; }


        [Required]
        [DisplayName("Kvittonummer")]
        public int ReceiptNumber { get; set; }

        [NotMapped]
        [DisplayName("Produktnamn")]
        public string? ProductName { get; set; } = string.Empty;
    }
}
