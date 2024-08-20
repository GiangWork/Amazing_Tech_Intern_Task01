using System.Collections.Generic;
using System.Threading.Tasks;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.UserModelViews;

namespace XuongMay.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<IList<UserResponseModel>> GetAllUsers();
        Task<UserResponseModel> GetUserById(int id);
        Task<UserResponseModel> UpdateUser(int id, UserUpdateModel model);
        Task<bool> DeleteUser(int id);
    }
}
