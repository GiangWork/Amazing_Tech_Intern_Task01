using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class UserInfo : BaseEntity
    {
        // Tên đầy đủ của người dùng, khởi tạo với giá trị rỗng
        public string FullName { get; set; } = string.Empty;

        // Số tài khoản ngân hàng của người dùng, có thể là null
        public string? BankAccount { get; set; }

        // Tên tài khoản ngân hàng của người dùng, có thể là null
        public string? BankAccountName { get; set; }

        // Ngân hàng của người dùng, có thể là null
        public string? Bank { get; set; }
    }
}
