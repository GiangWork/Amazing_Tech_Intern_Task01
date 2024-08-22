using System.Text.Json.Serialization;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Order : BaseEntity
    {
        // Tên đơn hàng, khởi tạo với giá trị rỗng
        public string OrderName { get; set; } = string.Empty;

        // Thời gian bắt đầu của đơn hàng
        public DateTime StartTime { get; set; }

        // Thời gian kết thúc của đơn hàng
        public DateTime EndTime { get; set; }

        // Số lượng sản phẩm trong đơn hàng
        public int Quantity { get; set; }

        // ID của sản phẩm, bắt buộc phải có
        public required string ProductID { get; set; }

        // Sản phẩm liên kết với đơn hàng, không được serialize trong JSON
        [JsonIgnore]
        public virtual Product? Product { get; set; }

        // Danh sách các tác vụ liên quan đến đơn hàng, không được serialize trong JSON
        [JsonIgnore]
        public virtual ICollection<OrderTask> OrderTask { get; set; } = new HashSet<OrderTask>();
    }
}
