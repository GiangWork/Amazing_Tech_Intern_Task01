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
        // Các trường riêng tư để lưu trữ các phụ thuộc được tiêm qua constructor
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        // Constructor để khởi tạo các phụ thuộc
        public OrderTaskService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Phương thức để tạo một nhiệm vụ đơn hàng mới
        public async Task<OrderTask> CreateOrderTask(OrderTaskModelView request)
        {
            // Chuyển đổi OrderTaskModelView thành OrderTask
            OrderTask OrderTask = _mapper.Map<OrderTask>(request);

            // Thêm nhiệm vụ đơn hàng vào cơ sở dữ liệu
            _context.OrderTasks.Add(OrderTask);
            await _context.SaveChangesAsync();
            return OrderTask;
        }

        // Phương thức để lấy tất cả nhiệm vụ đơn hàng với phân trang
        public async Task<BasePaginatedList<OrderTask>> GetAllOrderTasks(int pageNumber, int pageSize)
        {
            // Lấy tất cả nhiệm vụ đơn hàng từ cơ sở dữ liệu
            var allOrderTasks = await _context.OrderTasks.ToListAsync();

            // Tính tổng số mục và áp dụng phân trang
            var totalItems = allOrderTasks.Count();
            var items = allOrderTasks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tạo danh sách phân trang và trả về
            var paginatedList = new BasePaginatedList<OrderTask>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        // Phương thức để lấy nhiệm vụ đơn hàng theo ID
        public async Task<OrderTask> GetOrderTaskById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid order task id.", nameof(id));

            // Tìm nhiệm vụ đơn hàng theo ID
            var orderTask = await _context.OrderTasks.FirstOrDefaultAsync(ot => ot.Id == id);

            // Trả về giá trị mặc định nếu không tìm thấy (có thể cần xử lý khác tùy theo yêu cầu ứng dụng của bạn)
            return orderTask ?? throw new KeyNotFoundException("Order task not found.");
        }

        // Phương thức để cập nhật nhiệm vụ đơn hàng theo ID
        public async Task<OrderTask> UpdateOrderTask(string id, UpdateOrderTaskModelView request)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid order task id.", nameof(id));
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Tìm nhiệm vụ đơn hàng theo ID
            var orderTask = await _context.OrderTasks.FirstOrDefaultAsync(ot => ot.Id == id);
            if (orderTask == null)
            {
                throw new KeyNotFoundException("Order task not found."); // Hoặc trả về giá trị mặc định hoặc null tùy nhu cầu
            }

            // Cập nhật thông tin nhiệm vụ đơn hàng nếu có thay đổi từ UpdateOrderTaskModelView
            if (!string.IsNullOrWhiteSpace(request.OrderID))
            {
                orderTask.OrderID = request.OrderID;
            }

            if (!string.IsNullOrWhiteSpace(request.LineID))
            {
                orderTask.LineID = request.LineID;
            }

            if (request.Quantity.HasValue)
            {
                orderTask.Quantity = request.Quantity.Value;
            }

            // Cập nhật nhiệm vụ đơn hàng trong cơ sở dữ liệu
            _context.OrderTasks.Update(orderTask);
            await _context.SaveChangesAsync();
            return orderTask;
        }

        // Phương thức để xóa nhiệm vụ đơn hàng theo ID
        public async Task<bool> DeleteOrderTask(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid order task id.", nameof(id));

            // Tìm nhiệm vụ đơn hàng theo ID
            var orderTask = await _context.OrderTasks.FirstOrDefaultAsync(ot => ot.Id == id);
            if (orderTask == null)
            {
                return false;
            }

            // Xóa nhiệm vụ đơn hàng và lưu thay đổi
            _context.OrderTasks.Remove(orderTask);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
