using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class OrderStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderStatusID { get; set; }


        [Required]
        [StringLength(50)]
        [DisplayName("Status")]
        public string StatusTitel { get; set;} = string.Empty;


        [Required]
        [DisplayName("Levererad")]
        public bool Delivered { get; set; }


        [Required]
        [DisplayName("Skickad")]
        public bool Shipped { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
