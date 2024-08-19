using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.OrderTaskModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface IOrderTaskService
    {
        Task<OrderTask> CreateOrderTask(OrderTaskModelView request);
        Task<List<OrderTask>> GetAllOrderTasks();
        Task<OrderTask> GetOrderTaskById(string id);
        Task<OrderTask> UpdateOrderTask(string id, OrderTaskModelView request);
        Task<bool> DeleteOrderTask(string id);
    }
}
