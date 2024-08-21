using Microsoft.AspNetCore.Identity;
using XuongMay.Core.Utils;

namespace XuongMay.Contract.Repositories.Entity
{
    // Đại diện cho một quyền của vai trò với thông tin bổ sung về thời gian và người thực hiện các thao tác.
    public class ApplicationRoleClaims : IdentityRoleClaim<Guid>
    {
        // Người tạo quyền của vai trò.
        public string? CreatedBy { get; set; }

        // Người cập nhật quyền của vai trò lần cuối.
        public string? LastUpdatedBy { get; set; }

        // Người xóa quyền của vai trò.
        public string? DeletedBy { get; set; }

        // Thời gian tạo quyền của vai trò.
        public DateTimeOffset CreatedTime { get; set; }

        // Thời gian cập nhật quyền của vai trò lần cuối.
        public DateTimeOffset LastUpdatedTime { get; set; }

        // Thời gian xóa quyền của vai trò, có thể null nếu chưa bị xóa.
        public DateTimeOffset? DeletedTime { get; set; }

        public ApplicationRoleClaims()
        {
            // Khởi tạo thời gian tạo và thời gian cập nhật
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}
