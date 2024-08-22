using System.Security.Claims;
using XuongMay.ModelViews.AuthModelViews;
using XuongMay.Repositories.Entity;

namespace XuongMay.Services.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(ApplicationUser user);
        string AuthenticateUser(LoginModelView request);
        Task<string> CreateUser(RegisterModelView request);
        string ValidateLogin(LoginModelView request);
        string ValidateRegister(RegisterModelView request);
        Task<string> ChangePassword(ChangePasswordModelView request, ClaimsPrincipal userClaims);
    }
}
