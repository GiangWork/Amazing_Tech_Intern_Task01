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
        private readonly DatabaseContext _context; // Dùng để truy cập cơ sở dữ liệu
        private readonly IMapper _mapper; // Dùng để ánh xạ giữa các mô hình

        public UserService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Lấy danh sách người dùng với phân trang
        public async Task<BasePaginatedList<UserResponseModel>> GetAllUsers(int pageNumber, int pageSize)
        {
            var allUsers = await _context.ApplicationUsers.Include(u => u.UserInfo).ToListAsync(); // Lấy tất cả người dùng
            var totalItems = allUsers.Count(); // Tổng số người dùng
            var items = allUsers.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Lấy người dùng theo phân trang

            var userResponseModels = _mapper.Map<List<UserResponseModel>>(items); // Ánh xạ danh sách người dùng
            var paginatedList = new BasePaginatedList<UserResponseModel>(userResponseModels, totalItems, pageNumber, pageSize); // Tạo danh sách phân trang
            return paginatedList;
        }

        // Lấy thông tin người dùng theo ID
        public async Task<UserResponseModel> GetUserById(Guid id)
        {
            var userEntity = await _context.ApplicationUsers.Include(u => u.UserInfo).FirstOrDefaultAsync(u => u.Id == id); // Tìm người dùng theo ID
            if (userEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found."); // Ném lỗi nếu không tìm thấy người dùng
            }

            var userResponse = _mapper.Map<UserResponseModel>(userEntity); // Ánh xạ người dùng
            return userResponse;
        }

        // Cập nhật thông tin người dùng
        public async Task<UserInfo> UpdateUserInfo(UserInfoModel request, ClaimsPrincipal userClaims)
        {
            // Lấy ID người dùng từ claims
            var userIdClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new InvalidOperationException("Invalid user ID in claims."); // Ném lỗi nếu ID không hợp lệ
            }

            var applicationUser = await _context.ApplicationUsers.Include(u => u.UserInfo).FirstOrDefaultAsync(u => u.Id == userId); // Tìm người dùng theo ID

            if (applicationUser == null || applicationUser.UserInfo == null)
            {
                throw new InvalidOperationException("User not found."); // Ném lỗi nếu không tìm thấy người dùng
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
            _context.UserInfos.Update(userInfo); // Cập nhật thông tin người dùng
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            return userInfo; // Trả về thông tin người dùng đã cập nhật
        }

        // Xóa người dùng theo ID
        public async Task<bool> DeleteUser(Guid id)
        {
            var userEntity = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id); // Tìm người dùng theo ID
            if (userEntity == null)
            {
                return false; // Trả về false nếu không tìm thấy người dùng
            }

            _context.ApplicationUsers.Remove(userEntity); // Xóa người dùng
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return true; // Trả về true nếu xóa thành công
        }
    }
}
