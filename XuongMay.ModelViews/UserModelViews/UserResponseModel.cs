namespace XuongMay.ModelViews.UserModelViews
{
    public class UserInfoModel
    {
        // Thêm các thuộc tính cần thiết cho việc cập nhật người dùng
        public string? FullName { get; set; }
        public string? BankAccount { get; set; }
        public string? BankAccountName { get; set; }
        public string? Bank { get; set; }
    }

    public class UserResponseModel
    {
        // Các thuộc tính trả về cho người dùng
        public Guid Id { get; set; }
        public string? Username { get; set; }
    }
}
