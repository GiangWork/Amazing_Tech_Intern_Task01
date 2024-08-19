using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.OrderTaskModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface IOrderTaskService
    {
        Task<OrderTask> CreateOrderTask(OrderTask orderTask);
        Task<IEnumerable<OrderTask>> GetAllOrderTasks();
        Task<OrderTask?> GetOrderTaskById(string id);
        Task<OrderTask?> UpdateOrderTask(string id, OrderTask orderTask);
        Task<bool> DeleteOrderTask(string id);
        Task UpdateOrderTask(string id, OrderTaskModelView request);
        Task CreateOrderTask(OrderTaskModelView request);
    }
}
