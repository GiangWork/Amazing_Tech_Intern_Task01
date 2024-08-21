using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.RoleModelViews;
using XuongMay.ModelViews.UserRoleModelViews;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create_Role")]
        public async Task<IActionResult> CreateRole([FromQuery] RoleModelView request)
        {
            var Role = await _roleService.CreateRole(request);
            return Ok(new { Message = "Create Success", Role });
        }

        [HttpGet("get_AllRoles")]
        public async Task<IActionResult> GetAllRoles([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1;
            var pageSize = request.pageSize ?? 5;
            var productRoles = await _roleService.GetAllRoles(pageNumber, pageSize);
            if (productRoles == null)
                return NotFound(new { Message = "No Result" });
            return Ok(productRoles);
        }

        [HttpGet("get_RoleById/{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var Role = await _roleService.GetRoleById(id);
            if (Role == null)
            {
                return NotFound(new { Message = "No Result" });
            }
            return Ok(Role);
        }

        [HttpPut("update_Role/{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromQuery] UpdateRoleModelView request)
        {
            var Role = await _roleService.UpdateRole(id, request);
            if (Role == null)
            {
                return BadRequest(new { Message = "Update Fail" });
            }
            return Ok(new { Message = "Update Success", Role });
        }

        [HttpPut("assign_Role")]
        public async Task<IActionResult> AssignRole([FromQuery] UserRoleModelView request)
        {
            var UserRole = await _roleService.AssignRole(request);
            if (UserRole == null)
            {
                return BadRequest(new { Message = "Assign role Fail" });
            }
            return Ok(new { Message = "Assign role Success", UserRole });
        }

        [HttpDelete("delete_Role/{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _roleService.DeleteRole(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" });
            }
            return Ok(new { Message = "Delete Success" });
        }
    }
}
