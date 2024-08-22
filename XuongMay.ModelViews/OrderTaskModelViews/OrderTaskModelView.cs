namespace XuongMay.ModelViews.OrderTaskModelView
{
    public class OrderTaskModelView
    {
        // ID đơn hàng, không thể để trống
        public required string OrderID { get; set; }

        // ID dây chuyền sản xuất, không thể để trống
        public required string LineID { get; set; }

        // Số lượng, không thể âm
        public int Quantity { get; set; }
    }

    public class UpdateOrderTaskModelView
    {
        // ID đơn hàng có thể để trống nếu không cập nhật
        public string? OrderID { get; set; }

        // ID dây chuyền sản xuất có thể để trống nếu không cập nhật
        public string? LineID { get; set; }

        // Số lượng có thể để trống nếu không cập nhật
        public int? Quantity { get; set; }
    }
}
