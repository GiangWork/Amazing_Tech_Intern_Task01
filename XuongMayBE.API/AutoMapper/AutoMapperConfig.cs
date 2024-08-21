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
using XuongMay.Repositories.Entity;

namespace XuongMayBE.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Create AutoMapper
            CreateMap<ProductionLineModelView, ProductionLine>();

            CreateMap<CategoryModelView, Category>();

            CreateMap<OrderTaskModelView, OrderTask>();

            CreateMap<ProductModelView, Product>();

            CreateMap<OrderModelView, Order>();

            CreateMap<LoginModelView, ApplicationUser>();

            CreateMap<RoleModelView, ApplicationRole>();

            CreateMap<UserTokenModelView, ApplicationUserTokens>();

            CreateMap<UserLoginModelView, ApplicationUserLogins>();



            ////User
            CreateMap<UserUpdateModel, ApplicationUser>();
            CreateMap<ApplicationUser, UserResponseModel>();
            CreateMap<UserUpdateModel, ApplicationUser>();

        }
    }
}
