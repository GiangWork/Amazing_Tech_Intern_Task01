using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.OrderTaskModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTaskController : ControllerBase
    {
        private readonly IOrderTaskService _orderTaskService;
        private readonly IMapper _mapper;

        public OrderTaskController(IOrderTaskService orderTaskService, IMapper mapper)
        {
            _orderTaskService = orderTaskService;
            _mapper = mapper;
        }

        [HttpPost("create_OrderTask")]
        public async Task<IActionResult> CreateOrderTask([FromBody] OrderTaskModelView request)
        {
            var OrderTask = await _orderTaskService.CreateOrderTask(request);
            return Ok(OrderTask);
        }

        [HttpGet("get_AllOrderTasks")]
        public async Task<IActionResult> GetAllOrderTasks()
        {
            var OrderTasks = await _orderTaskService.GetAllOrderTasks();
            return Ok(OrderTasks);
        }

        [HttpGet("get_OrderTaskById/{id}")]
        public async Task<IActionResult> GetOrderTaskById(string id)
        {
            var OrderTask = await _orderTaskService.GetOrderTaskById(id);
            if (OrderTask == null)
            {
                return NotFound();
            }
            return Ok(OrderTask);
        }

        [HttpPut("update_OrderTask/{id}")]
        public async Task<IActionResult> UpdateOrderTask(string id, [FromBody] OrderTaskModelView request)
        {
            var OrderTask = await _orderTaskService.UpdateOrderTask(id, request);
            if (OrderTask == null)
            {
                return NotFound();
            }
            return Ok(OrderTask);
        }

        [HttpDelete("delete_OrderTask/{id}")]
        public async Task<IActionResult> DeleteOrderTask(string id)
        {
            var result = await _orderTaskService.DeleteOrderTask(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
