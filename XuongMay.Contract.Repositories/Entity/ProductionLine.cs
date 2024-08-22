using System.ComponentModel.DataAnnotations;
using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class ProductionLine : BaseEntity
    {
        // Tên của dây chuyền sản xuất, khởi tạo với giá trị rỗng
        public string LineName { get; set; } = string.Empty;

        // Số lượng công nhân, giới hạn độ dài tối đa là 50 ký tự
        [StringLength(50)]
        public int WorkerCount { get; set; }

        // Danh sách các tác vụ liên quan đến dây chuyền sản xuất này
        public virtual ICollection<OrderTask> OrderTask { get; set; } = new HashSet<OrderTask>();
    }
}
