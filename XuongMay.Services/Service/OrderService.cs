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
        // Các trường riêng tư để lưu trữ các phụ thuộc được tiêm qua constructor
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        // Constructor để khởi tạo các phụ thuộc
        public OrderService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Phương thức để tạo một đơn hàng mới
        public async Task<Order> CreateOrder(OrderModelView request)
        {
            // Chuyển đổi OrderModelView thành Order
            Order Order = _mapper.Map<Order>(request);

            // Thêm đơn hàng vào cơ sở dữ liệu
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
            return Order;
        }

        // Phương thức để lấy tất cả đơn hàng với phân trang
        public async Task<BasePaginatedList<Order>> GetAllOrders(int pageNumber, int pageSize)
        {
            // Lấy tất cả đơn hàng từ cơ sở dữ liệu
            var allCategories = await _context.Orders.ToListAsync();

            // Tính tổng số mục và áp dụng phân trang
            var totalItems = allCategories.Count();
            var items = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tạo danh sách phân trang và trả về
            var paginatedList = new BasePaginatedList<Order>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        // Phương thức để lấy đơn hàng theo ID
        public async Task<Order> GetOrderById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid order id.", nameof(id));

            // Tìm đơn hàng theo ID
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            return order;
        }

        // Phương thức để cập nhật đơn hàng theo ID
        public async Task<Order> UpdateOrder(string id, UpdateOrderModelView request)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid order id.", nameof(id));
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Tìm đơn hàng theo ID
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            // Cập nhật thông tin đơn hàng nếu có thay đổi từ UpdateOrderModelView
            if (!string.IsNullOrWhiteSpace(request.OrderName))
            {
                order.OrderName = request.OrderName;
            }

            if (!string.IsNullOrWhiteSpace(request.ProductID))
            {
                order.ProductID = request.ProductID;
            }

            if (request.Quantity.HasValue)
            {
                order.Quantity = request.Quantity.Value;
            }

            if (request.StartTime.HasValue)
            {
                order.StartTime = request.StartTime.Value;
            }

            if (request.EndTime.HasValue)
            {
                order.EndTime = request.EndTime.Value;
            }

            // Cập nhật đơn hàng trong cơ sở dữ liệu
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // Phương thức để xóa đơn hàng theo ID
        public async Task<bool> DeleteOrder(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid order id.", nameof(id));

            // Tìm đơn hàng theo ID
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            // Xóa đơn hàng và lưu thay đổi
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
