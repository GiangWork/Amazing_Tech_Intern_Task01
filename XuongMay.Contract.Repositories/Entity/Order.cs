using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product? Product { get; set; }
        public virtual ICollection<OrderProductionLine> OrderProductionLines { get; set; } = new HashSet<OrderProductionLine>();
    }
}
