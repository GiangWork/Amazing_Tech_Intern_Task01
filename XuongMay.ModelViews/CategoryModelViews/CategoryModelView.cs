namespace XuongMay.ModelViews.CategoryModelView
{
    public class CategoryModelView
    {
        // Tên danh mục, không thể để trống
        public string CategoryName { get; set; } = string.Empty;
    }

    public class UpdateCategoryModelView
    {
        // Tên danh mục có thể là null nếu không cần cập nhật
        public string? CategoryName { get; set; }
    }
}
