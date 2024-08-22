namespace XuongMay.ModelViews.AuthModelViews
{
    public class UserTokenModelView
    {
        // ID của người dùng, bắt buộc phải có
        public required Guid UserId { get; set; }

        // Giá trị của token, bắt buộc phải có
        public required string Value { get; set; }

        // Nhà cung cấp đăng nhập, bắt buộc phải có
        public required string LoginProvider { get; set; }

        // Tên của token, bắt buộc phải có
        public required string Name { get; set; }
    }
}
