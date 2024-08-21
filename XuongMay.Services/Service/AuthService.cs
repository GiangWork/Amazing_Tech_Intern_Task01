using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using XuongMay.Repositories.Entity;
using XuongMay.ModelViews.AuthModelViews;
using XuongMay.Repositories.Context;
using AutoMapper;
using XuongMay.Contract.Repositories.Entity;
using Microsoft.AspNetCore.Identity;

namespace XuongMay.Services.Service
{
    public class AuthService : IAuthService
    {
        // Các trường riêng tư để lưu trữ các phụ thuộc được tiêm qua constructor
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        // Constructor để khởi tạo các phụ thuộc
        public AuthService(IConfiguration configuration, DatabaseContext context, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Phương thức để tạo JWT token cho một người dùng cụ thể
        public string GenerateJwtToken(ApplicationUser user)
        {
            // Tạo một thể hiện mới của JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Lấy khóa bí mật từ cấu hình và chuyển đổi nó thành mảng byte
            var keyString = _configuration["JwtSettings:SecretKey"];

            if (keyString == null)
            {
                throw new ArgumentNullException("JwtSettings:SecretKey", "Secret key is not configured.");
            }
            var key = Encoding.ASCII.GetBytes(keyString);

            // Lấy vai trò của người dùng, nếu không có thì gán vai trò mặc định là "User"
            var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault() ?? "User";

            // Tạo mô tả cho token với các thông tin cần thiết
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                // Thời gian hết hạn của token được thiết lập trong cấu hình
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60")),
                Issuer = _configuration["JwtSettings:Issuer"] ?? string.Empty,
                Audience = _configuration["JwtSettings:Audience"] ?? string.Empty,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Tạo token và trả về dưới dạng chuỗi
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Phương thức để xác thực người dùng
        public string AuthenticateUser(LoginModelView request)
        {
            if (ValidateLogin(request) != string.Empty)
            {
                return ValidateLogin(request);
            }

            return "Login Success";
        }

        // Phương thức để tạo người dùng mới
        public async Task<string> CreateUser(RegisterModelView request)
        {
            if (ValidateRegister(request) != string.Empty)
            {
                return ValidateRegister(request);
            }

            ApplicationUser user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();

                // Lưu đối tượng UserInfo mới
                UserInfo userInfo = new UserInfo();

                _context.UserInfos.Add(userInfo);
                await _context.SaveChangesAsync(); // Lưu đối tượng UserInfo để có ID

                // Cập nhật UserInfoId trong ApplicationUser
                user.UserInfo = userInfo;
                _context.ApplicationUsers.Update(user);
                await _context.SaveChangesAsync(); // Lưu cập nhật trong ApplicationUser

                // Gắn role "User" cho người dùng
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                return "Registration Fail";
            }    

            return "Registration Success";
        }

        // Phương thức để thay đổi mật khẩu
        public async Task<string> ChangePassword(ChangePasswordModelView request, ClaimsPrincipal userClaims)
        {
            if (request == null || string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword))
            {
                return "Please provide all required fields.";
            }

            // Lấy ID người dùng từ claims
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return "User not found.";
            }

            // Tìm người dùng theo ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "User not found.";
            }

            // Kiểm tra mật khẩu cũ và thay đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return "Password changed successfully.";
            }
            else
            {
                return "Failed to change password: " + string.Join(", ", result.Errors.Select(e => e.Description));
            }
        }

        // Phương thức để xác thực thông tin đăng nhập
        public string ValidateLogin(LoginModelView request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return "Please fill all the field";
            }

            // Kiểm tra thông tin đăng nhập trong cơ sở dữ liệu
            if (_context.ApplicationUsers.SingleOrDefault(u => u.UserName == request.UserName && u.Password == request.Password) == null)
            {
                return "Invalid username or password";
            }

            return string.Empty;
        }

        // Phương thức để xác thực thông tin đăng ký
        public string ValidateRegister(RegisterModelView request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return "Please fill all the field";
            }

            // Kiểm tra nếu tên người dùng đã tồn tại trong cơ sở dữ liệu
            if (_context.ApplicationUsers.Any(u => u.UserName == request.UserName))
            {
                return "Username already exists";
            }

            return string.Empty;
        }

        
    }
}
