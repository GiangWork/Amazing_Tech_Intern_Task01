using Microsoft.EntityFrameworkCore;

namespace XuongMay.Contract.Repositories.Entity
{
    [PrimaryKey(nameof(OrderID), nameof(LineID))]
    public class OrderProductionLine
    {
        public string OrderID { get; set; }
        public string LineID { get; set; }
        public virtual Order Order { get; set; } = default!;
        public virtual ProductionLine ProductionLine { get; set; } = default!;
    }
}
