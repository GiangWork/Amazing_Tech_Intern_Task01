using Microsoft.AspNetCore.Identity;
using XuongMay.Core.Utils;

namespace XuongMay.Contract.Repositories.Entity
{
    public class ApplicationUserClaims : IdentityUserClaim<Guid>
    {
        // Người tạo quyền của người dùng
        public string? CreatedBy { get; set; }

        // Người cập nhật quyền của người dùng lần cuối
        public string? LastUpdatedBy { get; set; }

        // Người xóa quyền của người dùng
        public string? DeletedBy { get; set; }

        // Thời gian tạo quyền của người dùng
        public DateTimeOffset CreatedTime { get; set; }

        // Thời gian cập nhật quyền của người dùng lần cuối
        public DateTimeOffset LastUpdatedTime { get; set; }

        // Thời gian xóa quyền của người dùng, có thể null nếu chưa bị xóa
        public DateTimeOffset? DeletedTime { get; set; }

        public ApplicationUserClaims()
        {
            // Khởi tạo thời gian tạo và thời gian cập nhật
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}
