using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class CustomerRate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerRateID { get; set; }


        [Required]
        [ForeignKey("Products")]
        public int FK_ProductID { get; set; }
        public virtual Product? Products { get; set; }

       [StringLength(200)]
        public string RateDescription { get; set; } = string.Empty;

    }
}
