using System.ComponentModel.DataAnnotations;

namespace XuongMay.ModelViews.UserRoleModelViews
{
    public class UserRoleModelView
    {
        public required Guid UserId { get; set; }

        public required Guid RoleId { get; set; }
    }
}
