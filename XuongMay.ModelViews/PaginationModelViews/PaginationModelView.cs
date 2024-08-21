namespace XuongMay.ModelViews.PaginationModelView
{
    public class PaginationModelView
    {
        // Số trang hiện tại, giá trị mặc định là 1 nếu không được cung cấp
        public int? pageNumber { get; set; }

        // Kích thước trang, giá trị mặc định là 10 nếu không được cung cấp
        public int? pageSize { get; set; }
    }
}
