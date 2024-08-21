namespace XuongMay.ModelViews.AuthModelViews
{
    public class UserLoginModelView
    {
        // ID của người dùng, bắt buộc phải có
        public required Guid UserId { get; set; }

        // Khóa cung cấp cho đăng nhập, bắt buộc phải có
        public required string ProviderKey { get; set; }

        // Nhà cung cấp đăng nhập, bắt buộc phải có
        public required string LoginProvider { get; set; }
    }
}
