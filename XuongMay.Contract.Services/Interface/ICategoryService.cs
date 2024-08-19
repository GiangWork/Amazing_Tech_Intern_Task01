using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core;
using XuongMay.ModelViews.CategoryModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(CategoryModelView request);
        Task<BasePaginatedList<Category>> GetAllCategories(int pageNumber, int pageSize);
        Task<Category> GetCategoryById(string id);
        Task<Category> UpdateCategory(string id, CategoryModelView request);
        Task<bool> DeleteCategory(string id);
    }
}
