using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.CategoryModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface ICategoryService
    {
        Task<ProductCategory> CreateProductCategory(CategoryModelView request);
        Task<List<ProductCategory>> GetAllProductCategories();
        Task<ProductCategory> GetProductCategoryById(string id);
        Task<ProductCategory> UpdateProductCategory(string id, CategoryModelView request);
        Task<bool> DeleteProductCategory(string id);
    }
}
