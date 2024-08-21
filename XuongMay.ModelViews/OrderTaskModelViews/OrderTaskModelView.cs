namespace XuongMay.ModelViews.OrderTaskModelView
{
    public class OrderTaskModelView
    {
        public required string OrderID { get; set; }
        public required string LineID { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderTaskModelView
    {
        public string? OrderID { get; set; }
        public string? LineID { get; set; }
        public int? Quantity { get; set; }
    }
}
