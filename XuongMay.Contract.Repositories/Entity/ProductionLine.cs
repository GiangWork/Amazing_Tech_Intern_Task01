using System.ComponentModel.DataAnnotations;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class ProductionLine : BaseEntity
    {
        public string LineName { get; set; } = string.Empty;
        [StringLength(50)]
        public int WorkerCount { get; set; }
        public virtual ICollection<OrderProductionLine> OrderProductionLines { get; set; } = new HashSet<OrderProductionLine>();
    }
}
