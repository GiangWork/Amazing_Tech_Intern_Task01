using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuongMay.ModelViews.TaskModelViews
{
    public class TaskCreateModel
    {
        // Thêm các thuộc tính cần thiết cho việc tạo nhiệm vụ
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class TaskUpdateModel
    {
        // Thêm các thuộc tính cần thiết cho việc cập nhật nhiệm vụ
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class TaskResponseModel
    {
        // Các thuộc tính trả về cho nhiệm vụ
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
