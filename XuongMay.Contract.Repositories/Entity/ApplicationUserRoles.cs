using Microsoft.AspNetCore.Identity;
using XuongMay.Core.Utils;

namespace XuongMay.Contract.Repositories.Entity
{
    public class ApplicationUserRoles : IdentityUserRole<Guid>
    {
        // Người tạo vai trò của người dùng
        public string? CreatedBy { get; set; }

        // Người cập nhật vai trò của người dùng lần cuối
        public string? LastUpdatedBy { get; set; }

        // Người xóa vai trò của người dùng
        public string? DeletedBy { get; set; }

        // Thời gian tạo vai trò của người dùng
        public DateTimeOffset CreatedTime { get; set; }

        // Thời gian cập nhật vai trò của người dùng lần cuối
        public DateTimeOffset LastUpdatedTime { get; set; }

        // Thời gian xóa vai trò của người dùng, có thể null nếu chưa bị xóa
        public DateTimeOffset? DeletedTime { get; set; }

        public ApplicationUserRoles()
        {
            // Khởi tạo thời gian tạo và thời gian cập nhật
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}
