using System.ComponentModel.DataAnnotations;

namespace XuongMay.ModelViews.UserRoleModelViews
{
    public class UserRoleModelView
    {
        // ID của người dùng, không thể để trống
        public required Guid UserId { get; set; }

        // ID của vai trò, không thể để trống
        public required Guid RoleId { get; set; }
    }
}
