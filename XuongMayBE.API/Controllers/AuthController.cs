using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var authResult = _authService.AuthenticateUser(request);
            if (authResult == "Login Success")
            {
                ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(f => f.UserName == request.UserName);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                var existingToken = _context.ApplicationUserTokens
                    .FirstOrDefault(t => t.UserId == user.Id);

                string token;
                DateTime now = DateTime.UtcNow;

                if (existingToken == null || IsTokenExpired(existingToken.Value))
                {
                    // Token không tồn tại hoặc đã hết hạn, tạo token mới
                    token = _authService.GenerateJwtToken(user);

                    if (existingToken == null)
                    {
                        UserTokenModelView UserToken = new UserTokenModelView
                        {
                            UserId = user.Id,
                            Value = token,
                            LoginProvider = "Basic Authentication",
                            Name = "Basic Authentication"
                        };
                        ApplicationUserTokens ApplicationUserToken = _mapper.Map<ApplicationUserTokens>(UserToken);

                        await _context.ApplicationUserTokens.AddAsync(ApplicationUserToken);
                    }
                    else if (IsTokenExpired(existingToken.Value))
                    {
                        ApplicationUserTokens UpdateUserToken = _context.ApplicationUserTokens.FirstOrDefault(f => f.UserId == user.Id);
                        UpdateUserToken.Value = token;
                        UpdateUserToken.LastUpdatedTime = DateTime.UtcNow;
                        _context.ApplicationUserTokens.Update(UpdateUserToken);
                    }
                }
                else
                {
                    token = existingToken.Value;
                }

                ApplicationUserLogins UserLoginHistory = _context.ApplicationUserLogins.FirstOrDefault(f => f.UserId == user.Id);

                if (UserLoginHistory == null)
                {
                    UserLoginModelView UserLogin = new UserLoginModelView
                    {
                        UserId = user.Id,
                        ProviderKey = token,
                        LoginProvider = "Basic Authentication"
                    };
                    ApplicationUserLogins ApplicationUserLogin = _mapper.Map<ApplicationUserLogins>(UserLogin);

                    await _context.ApplicationUserLogins.AddAsync(ApplicationUserLogin);
                }
                else
                {
                    ApplicationUserLogins UpdateUserLogin = _context.ApplicationUserLogins.FirstOrDefault(f => f.UserId == user.Id);
                    UpdateUserLogin.LastUpdatedTime = DateTime.UtcNow;
                    _context.ApplicationUserLogins.Update(UpdateUserLogin);
                }

                await _context.SaveChangesAsync();
                
                return Ok(new { Message = "Login Success", Token = token });
            }

            return BadRequest(authResult);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] LoginModelView request)
        {
            if (await _authService.CreateUser(request) == "Registration Success")
            {
                return Ok(new { Message = "Registered success" });
            }

            return BadRequest(new { Message = _authService.CreateUser(request) });
        }

        private bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                return true; // Token không hợp lệ
            }

            // Kiểm tra thời gian hết hạn
            return jwtToken.ValidTo < DateTime.UtcNow;
        }
    }
}
