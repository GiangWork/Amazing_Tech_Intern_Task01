using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuongMay.Contract.Repositories.Entity;

namespace XuongMay.Contract.Services.Interface
{
    public interface IOrderTaskRepository
    {
        Task<OrderTask> AddAsync(OrderTask orderTask);
        Task<IEnumerable<OrderTask>> GetAllAsync();
        Task<OrderTask?> GetByIdAsync(string id);
        Task<OrderTask?> UpdateAsync(OrderTask orderTask);
        Task<bool> DeleteByIdAsync(string id);
    }
}
