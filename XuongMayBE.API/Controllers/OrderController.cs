using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.OrderTaskModelView;
using System.Collections.Generic;
using System.Threading.Tasks;
using XuongMay.ModelViews.OrderModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("create_Order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModelView request)
        {
            if (request == null)
            {
                return BadRequest("Invalid order data.");
            }

            var order = _mapper.Map<Order>(request);
            var result = await _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = result.OrderID }, _mapper.Map<OrderModelView>(result));
        }

        [HttpGet("get_AllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(_mapper.Map<IEnumerable<OrderModelView>>(orders));
        }

        [HttpGet("get_OrderById/{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderModelView>(order));
        }

        [HttpPut("update_Order/{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] OrderModelView request)
        {
            if (request == null)
            {
                return BadRequest("Invalid order data.");
            }

            var order = _mapper.Map<Order>(request);
            var result = await _orderService.UpdateOrder(id, order);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderModelView>(result));
        }

        [HttpDelete("delete_Order/{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
