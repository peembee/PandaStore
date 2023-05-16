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
        public string StatusTitel { get; set;}


        [Required]
        public bool Delivered { get; set; }


        [Required]
        public bool Shipped { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
