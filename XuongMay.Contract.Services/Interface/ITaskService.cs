using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuongMay.Contract.Repositories.Entity;

namespace XuongMay.Contract.Services.Interface
{
    public interface ITaskService
    {
        Task<IList<TaskResponseModel>> GetAllTasks();
        Task<TaskResponseModel> GetTaskById(int id);
        Task<TaskResponseModel> CreateTask(TaskCreateModel model);
        Task<TaskResponseModel> UpdateTask(int id, TaskUpdateModel model);
        Task<bool> DeleteTask(int id);
    }
}
