using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class Receipt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Kvitto ID")]
        public int ReceiptID { get; set; }

        [Required]
        public bool PaymentSucceeded { get; set; }


        [Required]
        [DisplayName("Kvittonummer")]
        public int ReceiptNumber { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
