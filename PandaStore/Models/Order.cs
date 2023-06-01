using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [DisplayName("Köpdatum")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [DisplayName("Totalpris")]
        public double OrderTotalPrice { get; set; }

        //[NotMapped]
        //public string? ReceiptNumber { get; set; } = string.Empty;

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
