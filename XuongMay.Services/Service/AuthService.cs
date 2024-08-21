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
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthService(IConfiguration configuration, DatabaseContext context, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                //Time JWT token expire is set in appseting.json (1 day)
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"])),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string AuthenticateUser(LoginModelView request)
        {
            if (ValidateLogin(request) != string.Empty)
            {
                return ValidateLogin(request);
            }

            return "Login Success";
        }

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

                // Gắn role "User" cho người dùng
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                return "Registration Fail";
            }    

            return "Registration Success";
        }

        public string ValidateLogin(LoginModelView request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return "Please fill all the field";
            }

            if (_context.ApplicationUsers.SingleOrDefault(u => u.UserName == request.UserName && u.Password == request.Password) == null)
            {
                return "Invalid username or password";
            }

            return string.Empty;
        }

        public string ValidateRegister(RegisterModelView request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return "Please fill all the field";
            }

            if (_context.ApplicationUsers.Any(u => u.UserName == request.UserName))
            {
                return "Username already exists";
            }

            return string.Empty;
        }
    }
}
