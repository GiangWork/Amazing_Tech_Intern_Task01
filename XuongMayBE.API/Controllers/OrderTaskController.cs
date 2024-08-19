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
            var orderTask = _mapper.Map<OrderTask>(request);
            var result = await _orderTaskService.CreateOrderTask(orderTask);
            return Ok(_mapper.Map<OrderTaskModelView>(result));
        }

        [HttpGet("get_AllOrderTasks")]
        public async Task<IActionResult> GetAllOrderTasks()
        {
            var orderTasks = await _orderTaskService.GetAllOrderTasks();
            return Ok(_mapper.Map<IEnumerable<OrderTaskModelView>>(orderTasks));
        }

        [HttpGet("get_OrderTaskById/{id}")]
        public async Task<IActionResult> GetOrderTaskById(string id)
        {
            var orderTask = await _orderTaskService.GetOrderTaskById(id);
            if (orderTask == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderTaskModelView>(orderTask));
        }

        [HttpPut("update_OrderTask/{id}")]
        public async Task<IActionResult> UpdateOrderTask(string id, [FromBody] OrderTaskModelView request)
        {
            var orderTask = _mapper.Map<OrderTask>(request);
            var result = await _orderTaskService.UpdateOrderTask(id, orderTask);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderTaskModelView>(result));
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
