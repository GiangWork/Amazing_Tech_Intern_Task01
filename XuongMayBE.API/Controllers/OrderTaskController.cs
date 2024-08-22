using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.OrderTaskModelView;
using XuongMay.ModelViews.PaginationModelView;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin, Line Manager")] // Cho phép người dùng có vai trò "Admin" và "Line Manager" truy cập
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTaskController : ControllerBase
    {
        private readonly IOrderTaskService _orderTaskService;

        public OrderTaskController(IOrderTaskService orderTaskService) => _orderTaskService = orderTaskService;

        [HttpPost("create_OrderTask")]
        public async Task<IActionResult> CreateOrderTask([FromQuery] OrderTaskModelView request)
        {
            // Tạo mới nhiệm vụ đơn hàng và trả về kết quả
            var OrderTask = await _orderTaskService.CreateOrderTask(request);
            return Ok(new { Message = "Create Success", OrderTask });
        }

        [HttpGet("get_AllOrderTasks")]
        public async Task<IActionResult> GetAllOrderTasks([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1; // Số trang, mặc định là 1 nếu không có
            var pageSize = request.pageSize ?? 5; // Số mục trên mỗi trang, mặc định là 5 nếu không có

            // Lấy tất cả nhiệm vụ đơn hàng với phân trang
            var OrderTasks = await _orderTaskService.GetAllOrderTasks(pageNumber, pageSize);

            if (OrderTasks == null)
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không có kết quả

            return Ok(OrderTasks); // Trả về danh sách nhiệm vụ đơn hàng
        }

        [HttpGet("get_OrderTaskById/{id}")]
        public async Task<IActionResult> GetOrderTaskById(string id)
        {
            // Lấy nhiệm vụ đơn hàng theo ID
            var OrderTask = await _orderTaskService.GetOrderTaskById(id);
            if (OrderTask == null)
            {
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không tìm thấy nhiệm vụ đơn hàng
            }
            return Ok(OrderTask); // Trả về nhiệm vụ đơn hàng tìm thấy
        }

        [HttpPut("update_OrderTask/{id}")]
        public async Task<IActionResult> UpdateOrderTask(string id, [FromQuery] UpdateOrderTaskModelView request)
        {
            // Cập nhật nhiệm vụ đơn hàng theo ID và trả về kết quả
            var OrderTask = await _orderTaskService.UpdateOrderTask(id, request);
            if (OrderTask == null)
            {
                return BadRequest(new { Message = "Update Fail" }); // Trả về lỗi nếu cập nhật không thành công
            }
            return Ok(new { Message = "Update Success", OrderTask });
        }

        [HttpDelete("delete_OrderTask/{id}")]
        public async Task<IActionResult> DeleteOrderTask(string id)
        {
            // Xóa nhiệm vụ đơn hàng theo ID và trả về kết quả
            var result = await _orderTaskService.DeleteOrderTask(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" }); // Trả về lỗi nếu xóa không thành công
            }
            return Ok(new { Message = "Delete Success" }); // Trả về thành công nếu xóa thành công
        }
    }
}
