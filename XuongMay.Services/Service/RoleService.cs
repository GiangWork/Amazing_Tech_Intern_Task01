using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.Core;
using XuongMay.ModelViews.RoleModelViews;
using XuongMay.ModelViews.UserRoleModelViews;
using XuongMay.Repositories.Context;

namespace XuongMay.Services.Service
{
    public class RoleService : IRoleService
    {
        private readonly DatabaseContext _context; // Dùng để truy cập cơ sở dữ liệu
        private readonly IMapper _mapper; // Dùng để ánh xạ giữa các mô hình

        public RoleService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Tạo một vai trò mới
        public async Task<ApplicationRole> CreateRole(RoleModelView request)
        {
            ApplicationRole ApplicationRole = _mapper.Map<ApplicationRole>(request); // Ánh xạ dữ liệu từ RoleModelView
            _context.ApplicationRoles.Add(ApplicationRole); // Thêm vai trò vào cơ sở dữ liệu
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return ApplicationRole; // Trả về vai trò đã tạo
        }

        // Lấy danh sách vai trò với phân trang
        public async Task<BasePaginatedList<ApplicationRole>> GetAllRoles(int pageNumber, int pageSize)
        {
            var allCategories = await _context.ApplicationRoles.ToListAsync(); // Lấy tất cả vai trò
            var totalItems = allCategories.Count(); // Tổng số vai trò
            var items = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Lấy vai trò theo phân trang

            var paginatedList = new BasePaginatedList<ApplicationRole>(items, totalItems, pageNumber, pageSize); // Tạo danh sách phân trang
            return paginatedList;
        }

        // Lấy thông tin vai trò theo ID
        public async Task<ApplicationRole> GetRoleById(Guid id)
        {
            var role = await _context.ApplicationRoles.FirstOrDefaultAsync(pc => pc.Id == id); // Tìm vai trò theo ID
            return role ?? new ApplicationRole(); // Trả về đối tượng mới nếu không tìm thấy
        }

        // Cập nhật thông tin vai trò theo ID
        public async Task<ApplicationRole> UpdateRole(Guid id, UpdateRoleModelView request)
        {
            ApplicationRole? ApplicationRole = await _context.ApplicationRoles.FirstOrDefaultAsync(pc => pc.Id == id); // Tìm vai trò theo ID
            if (ApplicationRole == null)
            {
                throw new KeyNotFoundException($"ApplicationRole with ID {id} was not found."); // Ném lỗi nếu không tìm thấy vai trò
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                ApplicationRole.Name = request.Name; // Cập nhật tên vai trò
            }

            _context.ApplicationRoles.Update(ApplicationRole); // Cập nhật vai trò
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return ApplicationRole; // Trả về vai trò đã cập nhật
        }

        // Gán vai trò cho người dùng
        public async Task<ApplicationUserRoles> AssignRole(UserRoleModelView request)
        {
            // Tìm thực thể người dùng và vai trò hiện tại
            var existingUserRole = await _context.ApplicationUserRoles
                .FirstOrDefaultAsync(pc => pc.UserId == request.UserId);

            if (existingUserRole != null)
            {
                // Xóa thực thể hiện tại nếu có
                _context.ApplicationUserRoles.Remove(existingUserRole); // Xóa vai trò hiện tại
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            }

            // Tạo mới thực thể với RoleId mới
            var newUserRole = new ApplicationUserRoles
            {
                UserId = request.UserId,
                RoleId = request.RoleId,
                CreatedBy = "system", // Hoặc lấy từ nguồn khác
                CreatedTime = DateTimeOffset.UtcNow // Thời gian tạo
            };

            _context.ApplicationUserRoles.Add(newUserRole); // Thêm vai trò người dùng mới
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            return newUserRole; // Trả về vai trò người dùng mới
        }

        // Xóa vai trò theo ID
        public async Task<bool> DeleteRole(Guid id)
        {
            ApplicationRole? ApplicationRole = await _context.ApplicationRoles.FirstOrDefaultAsync(pc => pc.Id == id); // Tìm vai trò theo ID
            if (ApplicationRole == null)
            {
                return false; // Trả về false nếu không tìm thấy vai trò
            }
            _context.ApplicationRoles.Remove(ApplicationRole); // Xóa vai trò
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return true; // Trả về true nếu xóa thành công
        }
    }
}
