using AutoMapper;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.CategoryModelView;
using XuongMay.ModelViews.OrderTaskModelView;
using XuongMay.ModelViews.ProductionLineModelViews;
using XuongMay.ModelViews.ProductModelView;

namespace XuongMayBE.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Create AutoMapper
            CreateMap<CreateProductionLineModelView, ProductionLine>();

            CreateMap<CategoryModelView, Category>();

            CreateMap<OrderTaskModelView, OrderTask>();

            CreateMap<ProductModelView, Product>();
        }
    }
}
