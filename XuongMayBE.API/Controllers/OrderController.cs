using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.OrderModelView;
using XuongMay.ModelViews.PaginationModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) => _orderService = orderService;

        [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập
        [HttpPost("create_Order")]
        public async Task<IActionResult> CreateOrder([FromQuery] OrderModelView request)
        {
            // Tạo mới đơn hàng và trả về kết quả
            var Order = await _orderService.CreateOrder(request);
            return Ok(new { Message = "Create success", Order });
        }

        [Authorize(Roles = "Admin, Line Manager")] // Cho phép người dùng có vai trò "Admin" và "Line Manager" truy cập
        [HttpGet("get_AllOrders")]
        public async Task<IActionResult> GetAllOrder([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1; // Số trang, mặc định là 1 nếu không có
            var pageSize = request.pageSize ?? 5; // Số mục trên mỗi trang, mặc định là 5 nếu không có

            // Lấy tất cả đơn hàng với phân trang
            var Orders = await _orderService.GetAllOrders(pageNumber, pageSize);

            if (Orders == null)
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không có kết quả

            return Ok(Orders); // Trả về danh sách đơn hàng
        }

        [Authorize(Roles = "Admin, Line Manager")] // Cho phép người dùng có vai trò "Admin" và "Line Manager" truy cập
        [HttpGet("get_OrderById/{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            // Lấy đơn hàng theo ID
            var Order = await _orderService.GetOrderById(id);
            if (Order == null)
            {
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không tìm thấy đơn hàng
            }
            return Ok(Order); // Trả về đơn hàng tìm thấy
        }

        [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập
        [HttpPut("update_Order/{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromQuery] UpdateOrderModelView request)
        {
            // Cập nhật đơn hàng theo ID và trả về kết quả
            var Order = await _orderService.UpdateOrder(id, request);
            if (Order == null)
            {
                return BadRequest(new { Message = "Update Fail" }); // Trả về lỗi nếu cập nhật không thành công
            }
            return Ok(new { Message = "Update Success", Order });
        }

        [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập
        [HttpDelete("delete_Order/{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            // Xóa đơn hàng theo ID và trả về kết quả
            var result = await _orderService.DeleteOrder(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" }); // Trả về lỗi nếu xóa không thành công
            }
            return Ok(new { Message = "Delete Success" }); // Trả về thành công nếu xóa thành công
        }
    }
}
