namespace XuongMay.ModelViews.UserModelViews
{
    public class UserCreateModel
    {
        // Thêm các thuộc tính cần thiết cho việc tạo người dùng
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class UserUpdateModel
    {
        // Thêm các thuộc tính cần thiết cho việc cập nhật người dùng
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class UserResponseModel
    {
        // Các thuộc tính trả về cho người dùng
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
