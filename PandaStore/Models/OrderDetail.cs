using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PandaStore.Models
{
    public class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }


        [Required]
        [ForeignKey("PandaUsers")]
        public string FK_PandaUserID { get; set; }
        public virtual PandaUser PandaUsers { get; set; }


        [Required]
        [ForeignKey("Orders")]
        public int FK_OrderID { get; set; }
        public virtual Order Orders { get; set; }


        [Required]
        [ForeignKey("OrderStatuses")]
        public int FK_OrderStatus { get; set; }
        public virtual OrderStatus OrderStatuses { get; set; }


        [Required]
        [ForeignKey("Receipts")]
        public int FK_ReceiptID { get; set; }
        public virtual Receipt Receipts { get; set; }

    }
}
