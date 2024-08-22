using XuongMay.Core.Base;

namespace XuongMay.Contract.Repositories.Entity
{
    public class AuditLog : BaseEntity
    {
        // Biểu mẫu hoặc loại biểu mẫu liên quan đến log
        public string? Form { get; set; }

        // Headers của yêu cầu HTTP
        public string? Headers { get; set; }

        // URL của yêu cầu HTTP
        public string? HttpURL { get; set; }

        // Địa chỉ IP nội bộ của máy khách
        public string? LocalAddress { get; set; }

        // Địa chỉ IP từ xa của máy khách
        public string? RemoteHost { get; set; }

        // Nội dung phản hồi từ máy chủ
        public string? ResponseBody { get; set; }

        // Mã trạng thái phản hồi của yêu cầu HTTP
        public string? ResponseStatusCode { get; set; }

        // Nội dung yêu cầu (Request Body)
        public string? ResQuestBody { get; set; }

        // Thông tin Claims của người dùng (thông tin xác thực)
        public string? Claims { get; set; }

        // ID của người dùng thực hiện yêu cầu
        public string? UserId { get; set; }

        // Tên của người dùng thực hiện yêu cầu
        public string? UserName { get; set; }
    }
}
