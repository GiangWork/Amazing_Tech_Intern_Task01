using System.ComponentModel.DataAnnotations;

namespace XuongMay.ModelViews.AuthModelViews
{
    public class LoginModelView
    {
        // Tên người dùng, bắt buộc phải có
        [Required]
        public required string UserName { get; set; }

        // Mật khẩu, bắt buộc phải có và phải đúng định dạng mật khẩu
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }

    public class ChangePasswordModelView
    {
        // Mật khẩu cũ, bắt buộc phải có và phải đúng định dạng mật khẩu
        [Required]
        [DataType(DataType.Password)]
        public required string OldPassword { get; set; }

        // Mật khẩu mới, bắt buộc phải có và phải đúng định dạng mật khẩu
        [Required]
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }
    }
}
