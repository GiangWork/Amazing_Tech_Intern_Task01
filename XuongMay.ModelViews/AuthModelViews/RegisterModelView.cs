﻿using System.ComponentModel.DataAnnotations;

namespace XuongMay.ModelViews.AuthModelViews
{
    public class RegisterModelView
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
