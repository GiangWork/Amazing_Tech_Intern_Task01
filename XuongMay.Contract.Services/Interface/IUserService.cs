using System.Security.Claims;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core;
using XuongMay.ModelViews.UserModelViews;

namespace XuongMay.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<BasePaginatedList<UserResponseModel>> GetAllUsers(int pageNumber, int pageSize);
        Task<UserResponseModel> GetUserById(Guid id);
        Task<UserInfo> UpdateUserInfo(UserInfoModel request, ClaimsPrincipal userClaims);
        Task<bool> DeleteUser(Guid id);
    }
}
