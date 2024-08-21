using XuongMay.Contract.Repositories.Entity;
using XuongMay.Core;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.ProductModelView;

namespace XuongMay.Contract.Services.Interface
{
    public interface IProductService
    {
        Task<Product> CreateProduct(ProductModelView request);
        Task<BasePaginatedList<Product>> GetAllProducts(int pageNumber, int pageSize);
        Task<Product> GetProductById(string id);
        Task<Product> UpdateProduct(string id, UpdateProductModelView request);
        Task<bool> DeleteProduct(string id);
    }
}
