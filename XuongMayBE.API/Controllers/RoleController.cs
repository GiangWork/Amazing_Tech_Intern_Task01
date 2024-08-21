using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.RoleModelViews;
using XuongMay.ModelViews.UserRoleModelViews;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService) => _roleService = roleService;

        [HttpPost("create_Role")]
        public async Task<IActionResult> CreateRole([FromQuery] RoleModelView request)
        {
            // Tạo mới vai trò và trả về kết quả
            var Role = await _roleService.CreateRole(request);
            return Ok(new { Message = "Create Success", Role });
        }

        [HttpGet("get_AllRoles")]
        public async Task<IActionResult> GetAllRoles([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1; // Số trang, mặc định là 1 nếu không có
            var pageSize = request.pageSize ?? 5; // Số mục trên mỗi trang, mặc định là 5 nếu không có

            // Lấy tất cả vai trò với phân trang
            var productRoles = await _roleService.GetAllRoles(pageNumber, pageSize);
            if (productRoles == null)
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không có kết quả

            return Ok(productRoles); // Trả về danh sách vai trò
        }

        [HttpGet("get_RoleById/{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            // Lấy vai trò theo ID
            var Role = await _roleService.GetRoleById(id);
            if (Role == null)
            {
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không tìm thấy vai trò
            }
            return Ok(Role); // Trả về vai trò tìm thấy
        }

        [HttpPut("update_Role/{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromQuery] UpdateRoleModelView request)
        {
            // Cập nhật vai trò theo ID và trả về kết quả
            var Role = await _roleService.UpdateRole(id, request);
            if (Role == null)
            {
                return BadRequest(new { Message = "Update Fail" }); // Trả về lỗi nếu cập nhật không thành công
            }
            return Ok(new { Message = "Update Success", Role });
        }

        [HttpPut("assign_Role")]
        public async Task<IActionResult> AssignRole([FromQuery] UserRoleModelView request)
        {
            // Gán vai trò cho người dùng và trả về kết quả
            var UserRole = await _roleService.AssignRole(request);
            if (UserRole == null)
            {
                return BadRequest(new { Message = "Assign role Fail" }); // Trả về lỗi nếu gán vai trò không thành công
            }
            return Ok(new { Message = "Assign role Success", UserRole });
        }

        [HttpDelete("delete_Role/{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            // Xóa vai trò theo ID và trả về kết quả
            var result = await _roleService.DeleteRole(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" }); // Trả về lỗi nếu xóa không thành công
            }
            return Ok(new { Message = "Delete Success" }); // Trả về thành công nếu xóa thành công
        }
    }
}
