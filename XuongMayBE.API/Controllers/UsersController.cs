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

        public UsersController(IUserService userService) => _userService = userService;

        // Chỉ cho phép người dùng có vai trò "Admin" truy cập
        [Authorize(Roles = "Admin")]
        [HttpGet("getAll_User")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1; // Số trang mặc định là 1 nếu không có
            var pageSize = request.pageSize ?? 2; // Số mục mỗi trang mặc định là 2 nếu không có

            // Lấy tất cả người dùng với phân trang
            var users = await _userService.GetAllUsers(pageNumber, pageSize);
            if (users == null)
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không có kết quả

            return Ok(users); // Trả về danh sách người dùng
        }

        // Chỉ cho phép người dùng có vai trò "Admin" truy cập
        [Authorize(Roles = "Admin")]
        [HttpGet("get_UserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            // Lấy thông tin người dùng theo ID
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không tìm thấy người dùng
            }

            return Ok(user); // Trả về thông tin người dùng
        }

        // Cho phép tất cả người dùng đã đăng nhập (authenticated) truy cập
        [Authorize]
        [HttpPut("update_UserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromQuery] UserInfoModel request)
        {
            var userClaims = HttpContext.User; // Lấy thông tin claims của người dùng hiện tại

            // Cập nhật thông tin người dùng và trả về kết quả
            var result = await _userService.UpdateUserInfo(request, userClaims);

            if (result == null)
            {
                // Trả về NotFound nếu người dùng không tìm thấy hoặc không có quyền
                return NotFound(new { Message = "User not found or user info not available." });
            }

            // Trả về thông tin đã cập nhật
            return Ok(new { Message = "Update success", result });
        }

        // Chỉ cho phép người dùng có vai trò "Admin" truy cập
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete_User/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // Xóa người dùng theo ID và trả về kết quả
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" }); // Trả về lỗi nếu xóa không thành công
            }

            return Ok(new { Message = "Delete Success" }); // Trả về thông báo thành công
        }
    }
}
