using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core;
using XuongMay.ModelViews.ProductionLineModelViews;

namespace XuongMay.Contract.Services.Interface
{
    public interface IProductionLineService
    {
        Task<ProductionLine> CreateProductionLine(ProductionLineModelView request);
        Task<BasePaginatedList<ProductionLine>> GetAllProductionLines(int pageNumber, int pageSize);
        Task<ProductionLine> GetProductionLineById(string id);
        Task<ProductionLine> UpdateProductionLine(string id, UpdateProductionLineModelView request);
        Task<bool> DeleteProductionLine(string id);
    }
}
