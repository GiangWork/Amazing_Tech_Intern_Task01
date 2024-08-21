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
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RoleService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplicationRole> CreateRole(RoleModelView request)
        {
            ApplicationRole ApplicationRole = _mapper.Map<ApplicationRole>(request);
            _context.ApplicationRoles.Add(ApplicationRole);
            await _context.SaveChangesAsync();
            return ApplicationRole;
        }

        public async Task<BasePaginatedList<ApplicationRole>> GetAllRoles(int pageNumber, int pageSize)
        {
            var allCategories = await _context.ApplicationRoles.ToListAsync();
            var totalItems = allCategories.Count();
            var items = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var paginatedList = new BasePaginatedList<ApplicationRole>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        public async Task<ApplicationRole> GetRoleById(Guid id)
        {
            return await _context.ApplicationRoles.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ApplicationRole> UpdateRole(Guid id, UpdateRoleModelView request)
        {
            ApplicationRole ApplicationRole = await _context.ApplicationRoles.FirstOrDefaultAsync(pc => pc.Id == id);
            if (ApplicationRole == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                ApplicationRole.Name = request.Name;
            }
            
            _context.ApplicationRoles.Update(ApplicationRole);
            await _context.SaveChangesAsync();
            return ApplicationRole;
        }

        public async Task<ApplicationUserRoles> AssignRole(UserRoleModelView request)
        {
            // Tìm thực thể hiện tại
            var existingUserRole = await _context.ApplicationUserRoles
                .FirstOrDefaultAsync(pc => pc.UserId == request.UserId);

            if (existingUserRole != null)
            {
                // Xóa thực thể hiện tại
                _context.ApplicationUserRoles.Remove(existingUserRole);
                await _context.SaveChangesAsync();
            }

            // Tạo mới thực thể với RoleId mới
            var newUserRole = new ApplicationUserRoles
            {
                UserId = request.UserId,
                RoleId = request.RoleId,
                CreatedBy = "system", // Hoặc lấy từ nguồn khác
                CreatedTime = DateTimeOffset.UtcNow
            };

            _context.ApplicationUserRoles.Add(newUserRole);
            await _context.SaveChangesAsync();

            return newUserRole;
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            ApplicationRole ApplicationRole = await _context.ApplicationRoles.FirstOrDefaultAsync(pc => pc.Id == id);
            if (ApplicationRole == null)
            {
                return false;
            }
            _context.ApplicationRoles.Remove(ApplicationRole);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
