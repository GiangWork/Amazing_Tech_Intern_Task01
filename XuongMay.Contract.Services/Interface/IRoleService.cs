using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core;
using XuongMay.ModelViews.RoleModelViews;

namespace XuongMay.Contract.Services.Interface
{
    public interface IRoleService
    {
        Task<ApplicationRole> CreateRole(RoleModelView request);
        Task<BasePaginatedList<ApplicationRole>> GetAllRoles(int pageNumber, int pageSize);
        Task<ApplicationRole> GetRoleById(Guid id);
        Task<ApplicationRole> UpdateRole(Guid id, RoleModelView request);
        Task<bool> DeleteRole(Guid id);
    }
}
