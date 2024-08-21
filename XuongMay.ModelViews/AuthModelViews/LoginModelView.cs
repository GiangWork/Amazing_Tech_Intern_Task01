using System.ComponentModel.DataAnnotations;

namespace XuongMay.ModelViews.AuthModelViews
{
    public class LoginModelView
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }

    public class ChangePasswordModelView
    {
        [Required]
        [DataType(DataType.Password)]
        public required string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }
    }
}
