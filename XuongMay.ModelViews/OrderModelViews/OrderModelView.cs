namespace XuongMay.ModelViews.OrderModelView
{
    public class OrderModelView
    {
        // Tên đơn hàng, không thể để trống
        public string OrderName { get; set; } = string.Empty;

        // ID sản phẩm, không thể để trống
        public required string ProductID { get; set; }

        // Số lượng, không thể âm
        public int Quantity { get; set; }

        // Thời gian bắt đầu
        public DateTime StartTime { get; set; }

        // Thời gian kết thúc
        public DateTime EndTime { get; set; }
    }

    public class UpdateOrderModelView
    {
        // Tên đơn hàng có thể để trống nếu không cập nhật
        public string? OrderName { get; set; }

        // ID sản phẩm có thể để trống nếu không cập nhật
        public string? ProductID { get; set; }

        // Số lượng có thể để trống nếu không cập nhật
        public int? Quantity { get; set; }

        // Thời gian bắt đầu có thể để trống nếu không cập nhật
        public DateTime? StartTime { get; set; }

        // Thời gian kết thúc có thể để trống nếu không cập nhật
        public DateTime? EndTime { get; set; }
    }
}
