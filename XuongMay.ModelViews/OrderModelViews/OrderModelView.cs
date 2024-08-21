namespace XuongMay.ModelViews.OrderModelView
{
    public class OrderModelView
    {
        public string OrderName { get; set; } = string.Empty;
        public required string ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class UpdateOrderModelView
    {
        public string? OrderName { get; set; }
        public string? ProductID { get; set; }
        public int? Quantity { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
