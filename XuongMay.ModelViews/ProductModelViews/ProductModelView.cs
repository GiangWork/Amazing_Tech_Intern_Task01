namespace XuongMay.ModelViews.ProductModelView
{
    public class ProductModelView
    {
        // Tên sản phẩm có thể để trống
        public string? ProductName { get; set; }

        // ID danh mục bắt buộc phải có giá trị
        public required string CategoryID { get; set; }
    }

    public class UpdateProductModelView
    {
        // Tên sản phẩm có thể để trống nếu không cập nhật
        public string? ProductName { get; set; }

        // ID danh mục có thể để trống nếu không cập nhật
        public string? CategoryID { get; set; }
    }
}
