namespace XuongMay.ModelViews.RoleModelViews
{
    public class RoleModelView
    {
        // Tên vai trò, không thể để trống
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateRoleModelView
    {
        // Tên vai trò có thể để trống nếu không cập nhật
        public string? Name { get; set; } = string.Empty;
    }
}
