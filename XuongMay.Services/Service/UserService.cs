using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Interface;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.UserModelViews;
using XuongMay.Repositories.Entity;
using XuongMay.Core;
using XuongMay.Repositories.Context;

namespace XuongMay.Services.Service
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BasePaginatedList<UserResponseModel>> GetAllUsers(int pageNumber, int pageSize)
        {
            var allUsers = await _context.ApplicationUsers.Include(u => u.UserInfo).ToListAsync();
            var totalItems = allUsers.Count();
            var items = allUsers.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var userResponseModels = _mapper.Map<List<UserResponseModel>>(items);
            var paginatedList = new BasePaginatedList<UserResponseModel>(userResponseModels, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        public async Task<UserResponseModel> GetUserById(Guid id)
        {
            var userEntity = await _context.ApplicationUsers.Include(u => u.UserInfo).FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity == null)
            {
                return null;
            }

            var userResponse = _mapper.Map<UserResponseModel>(userEntity);
            return userResponse;
        }

        public async Task<UserResponseModel> UpdateUser(Guid id, UserUpdateModel model)
        {
            var userEntity = await _context.ApplicationUsers.Include(u => u.UserInfo).FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity == null)
            {
                return null;
            }

            _mapper.Map(model, userEntity);
            _context.ApplicationUsers.Update(userEntity);
            await _context.SaveChangesAsync();

            var userResponse = _mapper.Map<UserResponseModel>(userEntity);
            return userResponse;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var userEntity = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity == null)
            {
                return false;
            }

            _context.ApplicationUsers.Remove(userEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
