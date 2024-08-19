using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.AuthModelViews;
using Microsoft.Extensions.Configuration;

namespace XuongMay.Services.Service
{
    public class AuthService : IAuthService
    {
        private static readonly List<User> Users = new List<User>();
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"])),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User AuthenticateUser(LoginModelView model)
        {
            return Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);
        }

        public void RegisterUser(LoginModelView model)
        {
            if (Users.Any(u => u.Username == model.Username))
            {
                throw new Exception("Username already exists");
            }

            Users.Add(new User { Username = model.Username, Password = model.Password });
        }

        public bool ValidateLoginModel(LoginModelView model, out string errorMessage)
        {
            errorMessage = null;
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                errorMessage = "Invalid login request";
                return false;
            }

            if (AuthenticateUser(model) == null)
            {
                errorMessage = "Invalid username or password";
                return false;
            }

            return true;
        }

        public bool ValidateRegisterModel(LoginModelView model, out string errorMessage)
        {
            errorMessage = null;
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                errorMessage = "Invalid registration request";
                return false;
            }

            if (Users.Any(u => u.Username == model.Username))
            {
                errorMessage = "Username already exists";
                return false;
            }

            return true;
        }
    }
}
