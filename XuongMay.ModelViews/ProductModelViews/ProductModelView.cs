namespace XuongMay.ModelViews.ProductModelView
{
    public class ProductModelView
    {
        public string? ProductName { get; set; }
        public required string CategoryID { get; set; }
    }

    public class UpdateProductModelView
    {
        public string? ProductName { get; set; }
        public string? CategoryID { get; set; }
    }
}
