using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PandaStore.Models
{
    public class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }


        [Required]
        [ForeignKey("PandaUsers")]
        public string Id { get; set; } = string.Empty;
        public virtual PandaUser? PandaUsers { get; set; }


        [Required]
        [ForeignKey("Orders")]
        [DisplayName("Order")]
        public int FK_OrderID { get; set; }
        public virtual Order? Orders { get; set; }


        [Required]
        [ForeignKey("OrderStatuses")]
        [DisplayName("Orderstatus")]
        public int FK_OrderStatus { get; set; }
        public virtual OrderStatus? OrderStatuses { get; set; }


        [Required]
        [ForeignKey("Receipts")]
        [DisplayName("Kvitto")]
        public int FK_ReceiptID { get; set; }
        public virtual Receipt? Receipts { get; set; }

    }
}
