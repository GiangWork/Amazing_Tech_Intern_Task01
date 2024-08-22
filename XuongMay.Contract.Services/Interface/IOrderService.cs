using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core;
using XuongMay.ModelViews.OrderModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(OrderModelView request);
        Task<BasePaginatedList<Order>> GetAllOrders(int pageNumber, int pageSize);
        Task<Order> GetOrderById(string id);
        Task<Order> UpdateOrder(string id, UpdateOrderModelView request);
        Task<bool> DeleteOrder(string id);
    }
}
