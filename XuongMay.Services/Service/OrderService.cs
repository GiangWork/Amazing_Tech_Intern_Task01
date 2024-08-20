using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.Core;
using XuongMay.ModelViews.OrderModelView;
using XuongMay.Repositories.Context;

namespace XuongMay.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrder(OrderModelView request)
        {
            Order Order = _mapper.Map<Order>(request);
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
            return Order;
        }

        public async Task<BasePaginatedList<Order>> GetAllOrders(int pageNumber, int pageSize)
        {
            var allCategories = await _context.Orders.ToListAsync();
            var totalItems = allCategories.Count();
            var items = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var paginatedList = new BasePaginatedList<Order>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _context.Orders.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<Order> UpdateOrder(string id, OrderModelView request)
        {
            Order Order = await _context.Orders.FirstOrDefaultAsync(pc => pc.Id == id);
            if (Order == null)
            {
                return null;
            }
            Order.OrderName = request.OrderName;
            Order.ProductID = request.ProductID;
            Order.Quantity = request.Quantity;
            Order.StartTime = request.StartTime;
            Order.EndTime = request.EndTime;
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();
            return Order;
        }

        public async Task<bool> DeleteOrder(string id)
        {
            Order Order = await _context.Orders.FirstOrDefaultAsync(pc => pc.Id == id);
            if (Order == null)
            {
                return false;
            }
            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
