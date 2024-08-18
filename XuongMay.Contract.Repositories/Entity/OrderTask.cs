using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace XuongMay.Contract.Repositories.Entity
{
    [PrimaryKey(nameof(OrderID), nameof(LineID))]
    public class OrderTask
    {
        public string OrderID { get; set; }
        public string LineID { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; } = default!;
        public virtual ProductionLine ProductionLine { get; set; } = default!;
    }
}
