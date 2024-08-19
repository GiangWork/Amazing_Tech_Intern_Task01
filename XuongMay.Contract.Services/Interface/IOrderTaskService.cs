using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core;
using XuongMay.ModelViews.OrderTaskModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface IOrderTaskService
    {
        Task<OrderTask> CreateOrderTask(OrderTaskModelView request);
        Task<BasePaginatedList<OrderTask>> GetAllOrderTasks(int pageNumber, int pageSize);
        Task<OrderTask> GetOrderTaskById(string id);
        Task<OrderTask> UpdateOrderTask(string id, OrderTaskModelView request);
        Task<bool> DeleteOrderTask(string id);
    }
}
