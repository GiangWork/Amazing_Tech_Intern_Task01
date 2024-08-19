using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.CategoryModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(CategoryModelView request);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(string id);
        Task<Category> UpdateCategory(string id, CategoryModelView request);
        Task<bool> DeleteCategory(string id);
    }
}
