using System.Text.Json.Serialization;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class Product : BaseEntity
    {
        // Tên sản phẩm, khởi tạo với giá trị rỗng
        public string ProductName { get; set; } = string.Empty;

        // Khóa ngoại liên kết với danh mục sản phẩm, bắt buộc phải có
        public required string CategoryID { get; set; }

        // Danh mục sản phẩm liên kết với sản phẩm này, không được serialize trong JSON
        [JsonIgnore]
        public virtual Category? Category { get; set; }

        // Danh sách đơn hàng liên quan đến sản phẩm này, không được serialize trong JSON
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
