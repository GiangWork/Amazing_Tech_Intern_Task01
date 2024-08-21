using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.UserModelViews;
using Microsoft.AspNetCore.Authorization;
using System;
using XuongMay.ModelViews.PaginationModelView;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.UpdateUser(id, model);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
