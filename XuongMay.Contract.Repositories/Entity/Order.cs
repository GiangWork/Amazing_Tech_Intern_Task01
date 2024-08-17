using System.ComponentModel.DataAnnotations.Schema;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Order : BaseEntity
    {
        public string OrderName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ProductID { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderProductionLine> OrderProductionLines { get; set; } = new HashSet<OrderProductionLine>();
    }
}
