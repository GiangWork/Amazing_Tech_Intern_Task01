using Microsoft.AspNetCore.Identity;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core.Utils;

namespace XuongMay.Repositories.Entity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Mật khẩu của người dùng, mặc định là chuỗi rỗng
        public string Password {  get; set; } = string.Empty;

        // Thông tin người dùng, có thể là null
        public virtual UserInfo? UserInfo { get; set; }

        // Người tạo người dùng
        public string? CreatedBy { get; set; }

        // Người cập nhật người dùng lần cuối
        public string? LastUpdatedBy { get; set; }

        // Người xóa người dùng
        public string? DeletedBy { get; set; }

        // Thời gian tạo người dùng
        public DateTimeOffset CreatedTime { get; set; }

        // Thời gian cập nhật người dùng lần cuối
        public DateTimeOffset LastUpdatedTime { get; set; }

        // Thời gian xóa người dùng, có thể null nếu chưa bị xóa
        public DateTimeOffset? DeletedTime { get; set; }

        public ApplicationUser()
        {
            // Khởi tạo thời gian tạo và thời gian cập nhật
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}
