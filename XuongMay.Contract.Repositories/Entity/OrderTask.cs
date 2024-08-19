using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    [PrimaryKey(nameof(OrderID), nameof(LineID))]
    public class OrderTask : BaseEntity
    {
        public required string OrderID { get; set; }
        public required string LineID { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; } = default!;

        [JsonIgnore]
        public virtual ProductionLine ProductionLine { get; set; } = default!;
    }
}
