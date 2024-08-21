namespace XuongMay.ModelViews.ProductionLineModelViews
{
    public class ProductionLineModelView
    {
        // Tên dây chuyền sản xuất, không thể để trống
        public string LineName { get; set; } = string.Empty;

        // Số lượng công nhân, không thể âm
        public int WorkerCount { get; set; }
    }

    public class UpdateProductionLineModelView
    {
        // Tên dây chuyền sản xuất có thể để trống nếu không cập nhật
        public string? LineName { get; set; }

        // Số lượng công nhân có thể để trống nếu không cập nhật
        public int? WorkerCount { get; set; }
    }
}
