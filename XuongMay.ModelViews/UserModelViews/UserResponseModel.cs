namespace XuongMay.ModelViews.UserModelViews
{
    public class UserInfoModel
    {
        // Tên đầy đủ của người dùng, có thể để trống nếu không cần thiết
        public string? FullName { get; set; }

        // Số tài khoản ngân hàng, có thể để trống nếu không có
        public string? BankAccount { get; set; }

        // Tên tài khoản ngân hàng, có thể để trống nếu không cần thiết
        public string? BankAccountName { get; set; }

        // Tên ngân hàng, có thể để trống nếu không cần thiết
        public string? Bank { get; set; }
    }

    public class UserResponseModel
    {
        // ID của người dùng, không thể để trống
        public Guid Id { get; set; }

        // Tên người dùng, có thể để trống nếu không có
        public string? Username { get; set; }
    }
}
