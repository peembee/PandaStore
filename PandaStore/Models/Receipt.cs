using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class Receipt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptID { get; set; }

        [Required]
        public bool PaymentSucceeded { get; set; }


        [Required]
        public int ReceiptNumber { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
