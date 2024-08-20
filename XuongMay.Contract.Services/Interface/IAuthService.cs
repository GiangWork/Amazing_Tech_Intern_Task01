using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.AuthModelViews;
using XuongMay.Repositories.Entity;

namespace XuongMay.Services.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(ApplicationUser user);
        string AuthenticateUser(LoginModelView request);
        Task<string> CreateUser(LoginModelView request);
        bool ValidateLogin(LoginModelView request, out string errorMessage);
        bool ValidateRegister(LoginModelView request, out string errorMessage);
    }
}
