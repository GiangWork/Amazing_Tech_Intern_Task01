using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuongMay.Contract.Repositories.Entity;

namespace XuongMay.Contract.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(string id);
        Task<Order> UpdateOrder(string id, Order order);
        Task<bool> DeleteOrder(string id);
    }
}
