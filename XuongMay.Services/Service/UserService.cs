using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Interface;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.UserModelViews;
using XuongMay.Repositories.Entity;
using XuongMay.Core;
using XuongMay.Repositories.Context;
using XuongMay.Contract.Repositories.Entity;
using System.Security.Claims;

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

        public async Task<UserInfo> UpdateUserInfo(UserInfoModel request, ClaimsPrincipal userClaims)
        {
            // Get the user ID from the claims
            var userIdClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return null; // Hoặc có thể ném một exception tùy thuộc vào yêu cầu
            }

            var applicationUser = await _context.ApplicationUsers.Include(u => u.UserInfo).FirstOrDefaultAsync(u => u.Id == userId);

            if (applicationUser == null || applicationUser.UserInfo == null)
            {
                return null; // Hoặc có thể ném một exception tùy thuộc vào yêu cầu
            }

            // Cập nhật các thuộc tính của UserInfo từ request
            var userInfo = applicationUser.UserInfo;
            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                userInfo.FullName = request.FullName;
            }
            if (!string.IsNullOrWhiteSpace(request.BankAccount))
            {
                userInfo.BankAccount = request.BankAccount;
            }
            if (!string.IsNullOrWhiteSpace(request.BankAccountName))
            {
                userInfo.BankAccountName = request.BankAccountName;
            }
            if (!string.IsNullOrWhiteSpace(request.Bank))
            {
                userInfo.Bank = request.Bank;
            }

            // Cập nhật cơ sở dữ liệu
            _context.UserInfos.Update(userInfo);
            await _context.SaveChangesAsync();

            return userInfo;
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
