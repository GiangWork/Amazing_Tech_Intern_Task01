using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuongMay.Contract.Repositories.Entity
{
    [Table("SanPham")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID {  get; set; }
        [StringLength(50)]
        public string ProductName { get; set; } = string.Empty;
        public int? CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
