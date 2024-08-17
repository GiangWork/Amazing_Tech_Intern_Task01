using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuongMay.Contract.Repositories.Entity
{
    public class ProductionLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineID { get; set; }
        [StringLength(255)]
        public string LineName { get; set; } = string.Empty;
        [StringLength(50)]
        public string LineCode { get; set; } = string.Empty;
        public int WorkerCount { get; set; }

        public virtual ICollection<OrderProductionLine> OrderProductionLines { get; set; } = new HashSet<OrderProductionLine>();
    }
}
