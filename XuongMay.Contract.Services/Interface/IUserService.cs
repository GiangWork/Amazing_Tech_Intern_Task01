using System.Collections.Generic;
using System.Threading.Tasks;
using XuongMay.Core;
using XuongMay.ModelViews.UserModelViews;

namespace XuongMay.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<BasePaginatedList<UserResponseModel>> GetAllUsers(int pageNumber, int pageSize);
        Task<UserResponseModel> GetUserById(Guid id);
        Task<UserResponseModel> UpdateUser(Guid id, UserUpdateModel model);
        Task<bool> DeleteUser(Guid id);
    }
}
