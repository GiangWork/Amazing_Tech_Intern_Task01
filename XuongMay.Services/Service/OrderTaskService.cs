using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.OrderTaskModelView;
using XuongMay.Repositories.Context;

namespace XuongMay.Services.Service
{
    public class OrderTaskService : IOrderTaskService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderTaskService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderTask> CreateOrderTask(OrderTaskModelView request)
        {
            OrderTask OrderTask = _mapper.Map<OrderTask>(request);
            _context.OrderTasks.Add(OrderTask);
            await _context.SaveChangesAsync();
            return OrderTask;
        }

        public async Task<List<OrderTask>> GetAllOrderTasks()
        {
            return await _context.OrderTasks.ToListAsync() ;
        }

        public async Task<OrderTask> GetOrderTaskById(string id)
        {
            return await _context.OrderTasks.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<OrderTask> UpdateOrderTask(string id, OrderTaskModelView request)
        {
            OrderTask OrderTask = await _context.OrderTasks.FirstOrDefaultAsync(pc => pc.Id == id);
            if (OrderTask == null)
            {
                return null;
            }
            OrderTask.OrderID = request.OrderID;
            OrderTask.LineID = request.LineID;
            OrderTask.Quantity = request.Quantity;
            await _context.SaveChangesAsync();
            return OrderTask;
        }

        public async Task<bool> DeleteOrderTask(string id)
        {
            OrderTask OrderTask = await _context.OrderTasks.FirstOrDefaultAsync(pc => pc.Id == id);
            if (OrderTask == null)
            {
                return false;
            }
            _context.OrderTasks.Remove(OrderTask);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<OrderTask> CreateOrderTask(OrderTask orderTask)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<OrderTask>> IOrderTaskService.GetAllOrderTasks()
        {
            throw new NotImplementedException();
        }

        public Task<OrderTask?> UpdateOrderTask(string id, OrderTask orderTask)
        {
            throw new NotImplementedException();
        }

        Task IOrderTaskService.UpdateOrderTask(string id, OrderTaskModelView request)
        {
            throw new NotImplementedException();
        }

        Task IOrderTaskService.CreateOrderTask(OrderTaskModelView request)
        {
            throw new NotImplementedException();
        }
    }
}
