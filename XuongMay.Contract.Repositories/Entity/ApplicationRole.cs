using Microsoft.AspNetCore.Identity;
using XuongMay.Core.Utils;

namespace XuongMay.Contract.Repositories.Entity
{
    // Đại diện cho một vai trò trong hệ thống với thông tin bổ sung về thời gian và người thực hiện các thao tác.
    public class ApplicationRole : IdentityRole<Guid>
    {
        // Người tạo vai trò.
        public string? CreatedBy { get; set; }

        // Người cập nhật vai trò lần cuối.
        public string? LastUpdatedBy { get; set; }

        // Người xóa vai trò.
        public string? DeletedBy { get; set; }

        // Thời gian tạo vai trò.
        public DateTimeOffset CreatedTime { get; set; }

        // Thời gian cập nhật vai trò lần cuối.
        public DateTimeOffset LastUpdatedTime { get; set; }

        // Thời gian xóa vai trò, có thể null nếu chưa bị xóa.
        public DateTimeOffset? DeletedTime { get; set; }

        public ApplicationRole()
        {
            // Khởi tạo thời gian tạo và thời gian cập nhật
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}