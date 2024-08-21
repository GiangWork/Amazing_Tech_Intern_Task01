using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.AuthModelViews;
using XuongMay.Repositories.Context;
using XuongMay.Repositories.Entity;
using XuongMay.Services.Service;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public AuthController(IAuthService authService, IMapper mapper, DatabaseContext context)
        {
            _authService = authService;
            _mapper = mapper;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginModelView request)
        {
            // Validate input
            if (request == null)
            {
                return BadRequest("Invalid login request."); // Trả về lỗi nếu yêu cầu không hợp lệ
            }

            var authResult = _authService.AuthenticateUser(request);
            if (authResult == "Login Success")
            {
                var user = await _context.ApplicationUsers
                    .FirstOrDefaultAsync(f => f.UserName == request.UserName); // Tìm người dùng theo tên

                if (user == null)
                {
                    return BadRequest("User not found"); // Trả về lỗi nếu không tìm thấy người dùng
                }

                var existingToken = await _context.ApplicationUserTokens
                    .FirstOrDefaultAsync(t => t.UserId == user.Id); // Tìm token hiện tại của người dùng

                string token;
                DateTime now = DateTime.UtcNow;

                // Kiểm tra token đã hết hạn hoặc không tồn tại
                if (!(existingToken != null && !IsTokenExpired(existingToken.Value ?? string.Empty)))
                {
                    token = _authService.GenerateJwtToken(user); // Tạo token mới

                    if (existingToken == null)
                    {
                        var userToken = new UserTokenModelView
                        {
                            UserId = user.Id,
                            Value = token,
                            LoginProvider = "Basic Authentication",
                            Name = "Basic Authentication"
                        };
                        var applicationUserToken = _mapper.Map<ApplicationUserTokens>(userToken);

                        await _context.ApplicationUserTokens.AddAsync(applicationUserToken); // Thêm token mới vào cơ sở dữ liệu
                    }
                    else
                    {
                        var updateUserToken = existingToken;
                        updateUserToken.Value = token;
                        updateUserToken.LastUpdatedTime = DateTime.UtcNow;
                        _context.ApplicationUserTokens.Update(updateUserToken); // Cập nhật token hiện tại
                    }
                }
                else
                {
                    token = existingToken.Value ?? string.Empty;    // Sử dụng token hiện tại nếu chưa hết hạn
                }

                var userLoginHistory = await _context.ApplicationUserLogins
                    .FirstOrDefaultAsync(f => f.UserId == user.Id); // Tìm lịch sử đăng nhập của người dùng

                if (userLoginHistory == null)
                {
                    var userLogin = new UserLoginModelView
                    {
                        UserId = user.Id,
                        ProviderKey = token,
                        LoginProvider = "Basic Authentication"
                    };
                    var applicationUserLogin = _mapper.Map<ApplicationUserLogins>(userLogin);

                    await _context.ApplicationUserLogins.AddAsync(applicationUserLogin); // Thêm lịch sử đăng nhập mới
                }
                else
                {
                    userLoginHistory.LastUpdatedTime = DateTime.UtcNow;
                    _context.ApplicationUserLogins.Update(userLoginHistory); // Cập nhật lịch sử đăng nhập hiện tại
                }

                await _context.SaveChangesAsync(); // Lưu tất cả thay đổi vào cơ sở dữ liệu

                return Ok(new { Message = "Login Success", Token = token }); // Trả về thành công và token
            }

            return BadRequest(authResult); // Trả về lỗi nếu đăng nhập không thành công
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] RegisterModelView request)
        {
            if (request == null)
            {
                return BadRequest("Invalid registration request."); // Trả về lỗi nếu yêu cầu không hợp lệ
            }

            string resultMessage = await _authService.CreateUser(request); // Tạo người dùng mới
            if (resultMessage == "Registration Success")
            {
                return Ok(new { Message = "Registered success" }); // Trả về thành công nếu đăng ký thành công
            }

            return BadRequest(new { Message = resultMessage }); // Trả về lỗi nếu đăng ký không thành công
        }

        [Authorize]
        [HttpPost("change_password")]
        public async Task<IActionResult> ChangePassword([FromQuery] ChangePasswordModelView model)
        {
            if (model == null)
            {
                return BadRequest("Invalid change password request."); // Trả về lỗi nếu yêu cầu không hợp lệ
            }

            var userClaims = HttpContext.User;
            var result = await _authService.ChangePassword(model, userClaims); // Thay đổi mật khẩu
            if (result == "Password changed successfully.")
            {
                return Ok(new { Message = result }); // Trả về thành công nếu thay đổi mật khẩu thành công
            }

            return BadRequest(new { Message = result }); // Trả về lỗi nếu thay đổi mật khẩu không thành công
        }

        private bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                return true; // Token không hợp lệ
            }

            return jwtToken.ValidTo < DateTime.UtcNow; // Kiểm tra xem token đã hết hạn chưa
        }
    }
}
