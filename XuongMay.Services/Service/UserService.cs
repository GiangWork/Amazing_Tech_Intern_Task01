using XuongMay.Contract.Repositories.Interface;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.UserModelViews;
using XuongMay.Contract.Repositories.Entity;

namespace XuongMay.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IList<UserResponseModel>> GetAllUsers()
        {
            IList<UserResponseModel> users = new List<UserResponseModel>
            {
                new UserResponseModel { Id = 1 },
                new UserResponseModel { Id = 2 },
                new UserResponseModel { Id = 3 }
            };

            return Task.FromResult(users);
        }

        public Task<UserResponseModel> GetUserById(int id)
        {
            // Sample implementation
            var user = new UserResponseModel { Id = id };
            return Task.FromResult(user);
        }

        public Task<UserResponseModel> CreateUser(UserCreateModel model)
        {
            // Sample implementation
            var user = new UserResponseModel { Id = model.Id };
            return Task.FromResult(user);
        }

        public Task<UserResponseModel> UpdateUser(int id, UserUpdateModel model)
        {
            // Sample implementation
            var user = new UserResponseModel { Id = id };
            //_context.ApplicationRoles.Update(ApplicationRole);
            return Task.FromResult(user);
        }

        public Task<bool> DeleteUser(int id)
        {
            // Sample implementation
            return Task.FromResult(true);
        }




    }
}
