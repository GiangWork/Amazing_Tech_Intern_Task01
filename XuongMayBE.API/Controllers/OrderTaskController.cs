using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.OrderTaskModelView;
using XuongMay.ModelViews.PaginationModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTaskController : ControllerBase
    {
        private readonly IOrderTaskService _orderTaskService;

        public OrderTaskController(IOrderTaskService orderTaskService)
        {
            _orderTaskService = orderTaskService;
        }

        [HttpPost("create_OrderTask")]
        public async Task<IActionResult> CreateOrderTask([FromQuery] OrderTaskModelView request)
        {
            var OrderTask = await _orderTaskService.CreateOrderTask(request);
            return Ok(new { Message = "Create Success", OrderTask });
        }

        [HttpGet("get_AllOrderTasks")]
        public async Task<IActionResult> GetAllOrderTasks([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1;
            var pageSize = request.pageSize ?? 2;
            var OrderTasks = await _orderTaskService.GetAllOrderTasks(pageNumber, pageSize);
            if (OrderTasks == null)
                return NotFound(new { Message = "No Result" });
            return Ok(OrderTasks);
        }

        [HttpGet("get_OrderTaskById/{id}")]
        public async Task<IActionResult> GetOrderTaskById(string id)
        {
            var OrderTask = await _orderTaskService.GetOrderTaskById(id);
            if (OrderTask == null)
            {
                return NotFound(new { Message = "No Result" });
            }
            return Ok(OrderTask);
        }

        [HttpPut("update_OrderTask/{id}")]
        public async Task<IActionResult> UpdateOrderTask(string id, [FromQuery] OrderTaskModelView request)
        {
            var OrderTask = await _orderTaskService.UpdateOrderTask(id, request);
            if (OrderTask == null)
            {
                return BadRequest(new { Message = "Update Fail" });
            }
            return Ok(new { Message = "Update Success", OrderTask });
        }

        [HttpDelete("delete_OrderTask/{id}")]
        public async Task<IActionResult> DeleteOrderTask(string id)
        {
            var result = await _orderTaskService.DeleteOrderTask(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" });
            }
            return Ok(new { Message = "Delete Success" });
        }
    }
}
