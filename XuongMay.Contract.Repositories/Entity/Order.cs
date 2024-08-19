using System.Text.Json.Serialization;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Order : BaseEntity
    {
        public string OrderName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Quantity { get; set; }
        public required string ProductID { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderTask> OrderTask { get; set; } = new HashSet<OrderTask>();
    }
}
