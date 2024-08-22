using AutoMapper;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.AuthModelViews;
using XuongMay.ModelViews.CategoryModelView;
using XuongMay.ModelViews.OrderModelView;
using XuongMay.ModelViews.OrderTaskModelView;
using XuongMay.ModelViews.ProductionLineModelViews;
using XuongMay.ModelViews.ProductModelView;
using XuongMay.ModelViews.RoleModelViews;
using XuongMay.ModelViews.UserModelViews;
using XuongMay.ModelViews.UserRoleModelViews;
using XuongMay.Repositories.Entity;

namespace XuongMayBE.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Create AutoMapper
            CreateMap<ProductionLineModelView, ProductionLine>();   // ProductionLineModelView -> ProductionLine

            CreateMap<CategoryModelView, Category>();   // CategoryModelView -> Category

            CreateMap<OrderTaskModelView, OrderTask>(); // OrderTaskModelView -> OrderTask

            CreateMap<ProductModelView, Product>(); // ProductModelView -> Product

            CreateMap<OrderModelView, Order>(); // OrderModelView -> Order

            CreateMap<LoginModelView, ApplicationUser>();   // LoginModelView -> ApplicationUser

            CreateMap<RegisterModelView, ApplicationUser>();    // RegisterModelView -> ApplicationUser

            CreateMap<RoleModelView, ApplicationRole>();    // RoleModelView -> ApplicationRole

            CreateMap<UserTokenModelView, ApplicationUserTokens>(); // UserTokenModelView -> ApplicationUserTokens

            CreateMap<UserLoginModelView, ApplicationUserLogins>(); // UserLoginModelView -> ApplicationUserLogins

            CreateMap<UserInfoModel, UserInfo>();   // UserInfoModel -> UserInfo

            CreateMap<ApplicationUser, UserResponseModel>();    // ApplicationUser -> UserResponseModel

            CreateMap<UserRoleModelView, ApplicationUserRoles>();    // UserRoleModelView -> ApplicationUserRoles
        }
    }
}
