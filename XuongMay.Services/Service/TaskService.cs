using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.TaskModelViews;
using XuongMay.Repositories.Context;

namespace XuongMay.Services.Service
{
    public class TaskService : ITaskService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public TaskService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<TaskResponseModel>> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return _mapper.Map<IList<TaskResponseModel>>(tasks);
        }

        public async Task<TaskResponseModel> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return _mapper.Map<TaskResponseModel>(task);
        }

        public async Task<TaskResponseModel> CreateTask(TaskCreateModel model)
        {
            var taskEntity = _mapper.Map<TaskEntity>(model);
            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskResponseModel>(taskEntity);
        }

        public async Task<TaskResponseModel> UpdateTask(int id, TaskUpdateModel model)
        {
            var taskEntity = await _context.Tasks.FindAsync(id);
            if (taskEntity == null)
            {
                return null;
            }

            _mapper.Map(model, taskEntity);
            _context.Tasks.Update(taskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskResponseModel>(taskEntity);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var taskEntity = await _context.Tasks.FindAsync(id);
            if (taskEntity == null)
            {
                return false;
            }

            _context.Tasks.Remove(taskEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
