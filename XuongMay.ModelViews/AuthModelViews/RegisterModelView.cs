using System.ComponentModel.DataAnnotations;

namespace XuongMay.ModelViews.AuthModelViews
{
    public class RegisterModelView
    {
        // Tên người dùng, bắt buộc phải có và không thể để trống
        [Required]
        public required string UserName { get; set; }

        // Mật khẩu, bắt buộc phải có và phải đúng định dạng mật khẩu
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
