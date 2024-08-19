using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.AuthModelViews;

namespace XuongMay.Services.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        User AuthenticateUser(LoginModelView model);
        void RegisterUser(LoginModelView model);
        bool ValidateLoginModel(LoginModelView model, out string errorMessage);
        bool ValidateRegisterModel(LoginModelView model, out string errorMessage);
    }
}
