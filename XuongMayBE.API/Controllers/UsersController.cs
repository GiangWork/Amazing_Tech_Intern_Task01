using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.UserModelViews;
using Microsoft.AspNetCore.Authorization;
using XuongMay.ModelViews.PaginationModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getAll_User")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1;
            var pageSize = request.pageSize ?? 2;
            var users = await _userService.GetAllUsers(pageNumber, pageSize);
            if (users == null)
                return NotFound(new { Message = "No Result" });
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get_UserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { Message = "No Result" });
            }
            return Ok(user);
        }

        [Authorize]
        [HttpPut("update_UserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromQuery] UserInfoModel request)
        {
            var userClaims = HttpContext.User;
            var result = await _userService.UpdateUserInfo(request, userClaims);

            if (result == null)
            {
                // Trả về NotFound nếu người dùng không tìm thấy hoặc không có quyền
                return NotFound(new { Message = "User not found or user info not available." });
            }

            // Trả về thông tin đã cập nhật
            return Ok(new { Message = "Update success", result });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete_User/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" });
            }

            return Ok(new { Message = "Delete Success" });
        }
    }
}
