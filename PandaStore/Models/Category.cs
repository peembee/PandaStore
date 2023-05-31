using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaStore.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Kategori ID")]
        public int CategoryId { get; set; }

        [StringLength(50)]
        [DisplayName("Varumärke")]
        public string CategoryTitel { get; set; } = string.Empty;

        public virtual ICollection<Product>? Products { get; set; }
    }
}
