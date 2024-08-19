using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.Core;
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

        public async Task<BasePaginatedList<OrderTask>> GetAllOrderTasks(int pageNumber, int pageSize)
        {
            var allOrderTasks = await _context.OrderTasks.ToListAsync();
            var totalItems = allOrderTasks.Count();
            var items = allOrderTasks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var paginatedList = new BasePaginatedList<OrderTask>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
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
    }
}
