using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    // Xác định khóa chính cho bảng với sự kết hợp của OrderID và LineID
    [PrimaryKey(nameof(OrderID), nameof(LineID))]
    public class OrderTask : BaseEntity
    {
        // ID của đơn hàng, bắt buộc phải có
        public required string OrderID { get; set; }

        // ID của dây chuyền sản xuất, bắt buộc phải có
        public required string LineID { get; set; }

        // Số lượng công việc trong đơn hàng
        public int Quantity { get; set; }

        // Đơn hàng liên kết với tác vụ này, không được serialize trong JSON
        [JsonIgnore]
        public virtual Order Order { get; set; } = default!;

        // Dây chuyền sản xuất liên kết với tác vụ này, không được serialize trong JSON
        [JsonIgnore]
        public virtual ProductionLine ProductionLine { get; set; } = default!;
    }
}
