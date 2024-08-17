using AutoMapper;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.ProductionLineModelViews;

namespace XuongMayBE.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Create AutoMapper
            CreateMap<CreateProductionLineModelView, ProductionLine>();
        }
    }
}
