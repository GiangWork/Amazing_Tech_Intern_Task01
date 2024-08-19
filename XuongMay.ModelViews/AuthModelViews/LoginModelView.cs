using System.ComponentModel.DataAnnotations;

namespace XuongMay.ModelViews.AuthModelViews
{
    public class LoginModelView
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
